using RedisLibrary;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueue _queue;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            IAddress address = new Address("localhost", "6379");
            RedisLibrary.IConfiguration configuration = new Configuration(address, "test-queue");
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);
            _queue = new Queue(connectionFactory);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
