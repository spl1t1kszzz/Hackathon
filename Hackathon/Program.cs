using Hackathon.Staff;
using Hackathon.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hackathon
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HackathonWorker>();
                    services.AddTransient<Model.Hackathon>();
                    services.AddTransient<ITeamBuildingStrategy, StableMarriageStrategy>();
                    services.AddTransient<HrManager>();
                    services.AddTransient<HrDirector>();
                })
                .Build();
            
            
            host.Run();
        }
    }
}