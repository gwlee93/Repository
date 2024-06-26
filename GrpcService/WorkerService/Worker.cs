using Application;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueue _queue;

        public Worker(ILogger<Worker> logger, IQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var result = _queue.DequeueAsync(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {                
                _logger.LogInformation(result.ToString());
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
