using Domain.MessageBus.Configuration;
using RabbitMQ.Client;
using IConnection = Domain.MessageBus.Connection.IConnection;
using IRabbitMQConnection = RabbitMQ.Client.IConnection;
namespace Infrastructure.Data.MessageBus.RabbitMQ
{
    internal class RabbitMQConnection : IConnection, IDisposable
    {
        public IModel Model { get; private set; } = default!;
        private IRabbitMQConnection _connection = default!;        
        private readonly IConfiguration _configuration;

        public RabbitMQConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void CloseConnection()
        {
            _connection.Close();
        }

        public void CreateConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration.Address.IP,
                Port = Convert.ToInt32(_configuration.Address.Port),
                UserName = _configuration.Address.UserName,
                Password = _configuration.Address.Password,
            };

            _connection = connectionFactory.CreateConnection();

            Model = _connection.CreateModel();

            Model.QueueDeclare(
                queue: _configuration.GetQueueName(),
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Dispose()
        {
            Model.Dispose();
            _connection.Dispose();
        }

        public IRabbitMQConnection GetConnection()
        {
            return _connection;
        }
    }
}
