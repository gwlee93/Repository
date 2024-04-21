using Application;
using Domain.MessageBus.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MessageBus.RabbitMQ
{
    public class RabbitMQQueue : IQueue
    {
        private readonly RabbitMQConnection _connection;
        private readonly string _queueName;
        private IModel _model = default!;
        public RabbitMQQueue(IConfiguration configuration)
        {
            _connection = new RabbitMQConnection(configuration);
            _connection.CreateConnection();
            _queueName = configuration.GetQueueName();
            _model = _connection.Model;
            
            _model.ConfirmSelect();
        }
        public async Task<string> DequeueAsync(CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<string>();
            string message = "";

            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += (model, eventArgs) =>
            {
                try
                {
                    var body = eventArgs.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Recevied: {message}");

                    // 메시지 처리 후 ack를 보내 큐에서 메시지 제거
                    _model.BasicAck(
                        deliveryTag: eventArgs.DeliveryTag,
                        multiple: false);

                    // 메시지 처리가 완료되면 TaskCompletionSource를 통해 결과를 설정
                    tcs.TrySetResult(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    _model.BasicNack(
                        deliveryTag: eventArgs.DeliveryTag,
                        multiple: false,
                        requeue: true);

                    // 오류 발생 시 TaskCompletionSource를 통해 예외를 설정
                    tcs.TrySetException(ex);
                }
            };

            // 메시지가 큐에 도착할 때마다 실시간으로 큐에서 연속적 소비 (시간 지연 최소화)
            // 서버와 연결이 유지되는 동안 메시지를 지속적으로 수신하도록 구독, 메시지를 가져오기 위해 수동으로 호출할 필요가 없음
            // 메시지가 큐에 도착하면 자동으로 consumer에게 전달되고, 전달된 메시지는 Received 이벤트 핸들러에서 처리 가능
            _model.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);

            // 취소 요청이 들어오면 Task가 취소되도록 등록
            cancellationToken.Register(() => tcs.TrySetCanceled());

            return await tcs.Task;
        }

        public async Task<long> EnqueueAsync(string value, CancellationToken cancellationToken = default)
        {
            var body = Encoding.UTF8.GetBytes(value);

            await Task.Run(() =>
            {
                _model.BasicPublish(
                    exchange: "lkw.test.exchange",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: body);
            }, cancellationToken);

            // 메시지가 RabbitMQ에 의해 수신되었는지 확인 (5초동안 대기)
            bool isConfirmed = _model.WaitForConfirms(new TimeSpan(0, 0, 5));

            if (isConfirmed == false)
            {
                throw new Exception("Message could not be confirmed.");
            }
            
            return await Task.FromResult(0L);
        }
    }
}
