namespace WorkerService.Extensions
{
    public static class HostedServiceExtension
    {
        public static IServiceCollection AddWorkerService(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();

            return services;
        }
    }
}
