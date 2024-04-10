using Domain.MessageBus.Configuration;
using Domain.MessageBus.Connection;
using StackExchange.Redis;

namespace WorkerService.MessageBus.Redis
{
    public class RedisConnection : IConnection, IDisposable
    {
        private IConnectionMultiplexer _connection = default!;
        private readonly IConfiguration _configuration;
        public RedisConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnectionMultiplexer GetConnection()
        {
            return _connection;
        }
        
        public void CloseConnection()
        {
            _connection.Close();
        }

        public void CreateConnection()
        {
            _connection = ConnectionMultiplexer.Connect(_configuration.GetAddress());
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
