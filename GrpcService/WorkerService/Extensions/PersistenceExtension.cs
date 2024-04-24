using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace WorkerService.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<StudentDbContext>();
            services.AddDbContextFactory<StudentDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreDb"),
                        b => b.MigrationsAssembly("WorkerService"))
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

            return services;
        }
        //public static IServiceCollection AddConfiguration(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        //{
        //    services.Configure<MessageBusOptions>(configuration.GetSection("MessageBusConfiguration"));
        //    services.AddSingleton<Domain.MessageBus.Configuration.IConfiguration>(provider =>
        //    {
        //        var config = provider.GetRequiredService<IOptions<MessageBusOptions>>().Value;
        //        var address = new Address(config.IP, config.Port.ToString(), config.UserName, config.Password);
        //        return new Configuration(address, config.QueueName);
        //    });

        //    return services;
        //}

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
