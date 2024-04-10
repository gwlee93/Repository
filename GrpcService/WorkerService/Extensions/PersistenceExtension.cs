using Application;
using Domain.MessageBus.Address;
using Domain.MessageBus.Configuration;
using Infrastructure.Data.MessageBus.RabbitMQ;
using WorkerService.MessageBus.Redis;

namespace WorkerService.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {            
            services.AddRabbitMQConfiguration();
            //services.AddSingleton<IQueue, RedisQueue>();
            services.AddSingleton<IQueue, RabbitMQQueue>();
            return services;
        }
        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
            {
                var address = new Address("localhost", "6379", "lkw", "1234");
                return new Configuration(address, "test-queue");
            });

            return services;
        }
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
            {
                var address = new Address("localhost", "5672", "lkw", "1234");
                return new Configuration(address, "test-queue");
            });

            return services;
        }
    }
}
