using WorkerService.Extensions;
using Microsoft.AspNetCore.Builder;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration Ãß°¡
            var configuration = new ConfigurationManager()
            .AddJsonFile("settings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

            builder.Services.AddGrpc();
            builder.Services.AddMapper();
            builder.Services.AddMediatR();
            builder.Services.AddWorkerService();
            builder.Services.AddRepositories();
            builder.Services.AddOptionExtension(configuration);
            builder.Services.AddMessageBus(configuration);            
            builder.Services.AddEFCore(configuration);

            var app = builder.Build();

            app.AddControllers();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            app.Run();
    
        }
    }
}