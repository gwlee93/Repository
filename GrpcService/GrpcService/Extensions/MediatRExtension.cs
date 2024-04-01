using GrpcService.MiddleWares;
using MediatR;
using System.Reflection;

namespace GrpcService.Extensions
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }      
    }
}
