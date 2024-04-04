using Infrastructure.Mappers.AutoMappers;

namespace WorkerService.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
            //services.AddMapster();
            return services;
        }
    }
}
