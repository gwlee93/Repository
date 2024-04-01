using Microsoft.AspNetCore.Hosting;
using WorkerService.Services;
using WorkerService.Extensions;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddHostedService<Worker>();                                            
                      services.AddScoped<RedisService>();
                      services.AddMediatR();
                  })
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseStartup<Startup>();
                  })
                .Build();

            host.Run();
        }
    }
}