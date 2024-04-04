using GrpcService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{
    public static class ControllerExtension
    {
        public static IEndpointRouteBuilder AddControllers(this IEndpointRouteBuilder app)
        {
            app.MapGrpcService<StudentService>();
            return app;
        }
    }
}
