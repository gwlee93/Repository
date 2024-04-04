using EntityStudent = Domain.Entities.Student;
using RedisLibrary;
using Domain.Entities;

namespace WorkerService.Services
{
    public class RedisService 
    {
        private readonly ILogger<RedisService> _logger;
        private readonly IQueue _queue;
        public RedisService(ILogger<RedisService> logger)
        {
            _logger = logger;

            IAddress address = new Address("localhost", "6379");
            RedisLibrary.IConfiguration configuration = new Configuration(address, "test-queue");
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);
            _queue = new Queue(connectionFactory);
        }
        public async Task EnqueueAsync(EntityStudent student)
        {
             _queue.Enqueue(student.StudentId);
            _logger.LogInformation(student.StudentId);
            await Task.CompletedTask;
        }    
        public async Task<string> DequeueAsync()
        {
            var item = _queue.Dequeue();
            _logger.LogInformation(item);
            return await Task.FromResult(item);            
        }     
    }
}
