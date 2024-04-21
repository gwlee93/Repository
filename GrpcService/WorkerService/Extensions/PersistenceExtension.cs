using Application;
using Domain.MessageBus.Address;
using Domain.MessageBus.Configuration;
using Domain.MessageBus.Options;
using Infrastructure.Data.MessageBus.RabbitMQ;
using Microsoft.Extensions.Options;

namespace WorkerService.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {            
            services.AddConfiguration(configuration);
            //services.AddSingleton<IQueue, RedisQueue>();
            services.AddSingleton<IQueue, RabbitMQQueue>();
            return services;
        }
        public static IServiceCollection AddConfiguration(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.Configure<MessageBusOptions>(configuration.GetSection("MessageBusConfiguration"));
            services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
            {
                var config = provider.GetRequiredService<IOptions<MessageBusOptions>>().Value;
                var address = new Address(config.IP, config.Port.ToString(), config.UserName, config.Password);
                return new Configuration(address, config.QueueName);
            });

            return services;
        }
 
        //public static IServiceCollection AddRedisConfiguration(this IServiceCollection services)
        //{
        //    services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
        //    {
        //        var address = new Address("localhost", "6379", "lkw", "1234");
        //        return new Configuration(address, "test-queue");
        //    });

        //    return services;
        //}
        //public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services)
        //{
        //    services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
        //    {
        //        var address = new Address("localhost", "5672", "lkw", "1234");
        //        return new Configuration(address, "test-queue");
        //    });

        //    return services;
        //}
    }
}
