using Polly;
using StackExchange.Redis;

namespace RedisLibrary
{
    public interface IConnectionFactory
    {
        public IConfiguration Configuration { get; set; }
        public IConnectionMultiplexer CreateConnection();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public IConfiguration Configuration { get; set; }

        public ConnectionFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    public IConnectionMultiplexer CreateConnection()
    {
        // Polly 재시도 정책 정의
        var retryPolicy = Policy
            .Handle<Exception>() // 예외를 처리할 유형 지정
            .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // 재시도 간격

        // 재시도 정책을 사용하여 연결 시도
        var multiplexer = retryPolicy.Execute(() =>
        {
            return ConnectionMultiplexer.Connect(Configuration.GetAddress());
        });

        return multiplexer;
    }
    }
}
