using Application;
using Domain.MessageBus.Configuration;

namespace WorkerService.MessageBus.Redis
{
    public class RedisQueue : IQueue
    {
        private readonly RedisConnection _connection;
        private readonly string _queueName;
        public RedisQueue(IConfiguration configuration)
        {
            _connection = new RedisConnection(configuration);
            _connection.CreateConnection();
            _queueName = configuration.GetQueueName();
        }
        public async Task<string> DequeueAsync(CancellationToken cancellationToken = default)
        {
            var result = await _connection.GetConnection().GetDatabase().ListRightPopAsync(_queueName);
            
            if (result.IsNull)
                throw new InvalidOperationException("Queue is empty.");

            return result.ToString();
        }

        public async Task<long> EnqueueAsync(string value, CancellationToken cancellationToken = default)
        {
            return await _connection.GetConnection().GetDatabase().ListLeftPushAsync(_queueName, value);
        }
    }
}
