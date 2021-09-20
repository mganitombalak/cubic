using System;
using System.Threading.Tasks;
using LandingCoordinator.Operations;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace LandingCoordinator.Server
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return RunServerAsync().Result;
        }

        private static async Task<int> RunServerAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press any key to terminate...\n\n");
                Console.ReadLine();
                await host.StopAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "landing-coordinator";
                    options.ServiceId = "CubicTelecom";
                })
                .ConfigureApplicationParts(parts =>
                    parts.AddApplicationPart(typeof(LandingOperation).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}