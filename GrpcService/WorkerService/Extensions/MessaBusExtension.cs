using Application;
using Domain.MessageBus.Address;
using Domain.MessageBus.Configuration;
using Domain.MessageBus.Options;
using Infrastructure.Data.MessageBus.RabbitMQ;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{
    public static class MessaBusExtension
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
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
    }
}
