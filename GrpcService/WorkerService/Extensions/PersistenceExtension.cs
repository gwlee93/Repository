using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorkerService.MiddleWares;
using WorkerService.Services;

namespace WorkerService.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<RedisService>();

            return services;
        }
    }
}
