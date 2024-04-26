using Application;
using Domain.MessageBus.Address;
using Domain.MessageBus.Options;
using Infrastructure.Data.MessageBus.Options;
using Microsoft.Extensions.Options;

namespace WorkerService.Extensions
{
    public static class OptionExtension
    {
        public static IServiceCollection AddOptionExtension(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddJsonFile(configuration);
            services.AddOption<MessageBusOptions>(configuration);
            return services;
        }
        private static IServiceCollection AddJsonFile(this IServiceCollection services, IConfigurationRoot configuration)
        {
            (configuration as ConfigurationManager).AddJsonFile("settings.json", false, true)
                                                   .AddEnvironmentVariables();                                                   

            return services;
        }

        private static IServiceCollection AddOption<T>(this IServiceCollection services, IConfigurationRoot configuration) where T : class, new()
        {
            var section = configuration.GetSection(typeof(T).Name);
            if (section == null)
                return services;
            services.Configure<T>(section);
            services.AddTransient<IOptional<T>>(provider =>
            {
                var options = provider.GetService<IOptionsMonitor<T>>()!;
                return new Optional<T>(options, configuration, section.Key, "settings.json");
            });

            return services;
        }
    }
}
