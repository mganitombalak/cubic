using System;
using System.Threading.Tasks;
using LandingCoordinator.Abstractions.Interfaces;
using LandingCoordinator.Engine;
using LandingCoordinator.Engine.Requests;
using LandingCoordinator.Engine.Vector;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

// in the memory of Alan Shepard
namespace LandingCoordinator.Shepard
{
    public class Program
    {
        private static int Main(string[] args)
        {
            return RunClientAsync().Result;
        }

        private static async Task<int> RunClientAsync()
        {
            try
            {
                await using var client = await ConnectClient();
                await StartLandingProcess(client);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the landing coordinator the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "landing-coordinator";
                    options.ServiceId = "CubicTelecom";
                })
                .ConfigureLogging(logging => logging.AddConsole()).Build();
            await client.Connect();
            Console.WriteLine("Client successfully connected to landing coordinator. \n");
            return client;
        }

        private static async Task StartLandingProcess(IClusterClient client)
        {
            var operations = client.GetGrain<ILandingOperation>(0);
            var landingAreaParameter = new LandingAreaParamaters
            {
                Name = "Kennedy Space Station",
                Size = 100,
                PlatformPosition = new VectorElement<int> { i = 5, j = 5 },
                PlatformSize = 5
            };
            await operations.SetLandingAreParamters(landingAreaParameter);
            do
            {
                Console.WriteLine("Enter coords(to exit enter minus -1)");
                Console.Write("x:");
                var x = int.Parse(Console.ReadLine());
                if (x == -1)
                    break;
                Console.Write("\ny:");
                var y = int.Parse(Console.ReadLine());
                if (y == -1)
                    break;
                var request = new AllocateRequest<int> { Position = new VectorElement<int> { i = x, j = y } };
                var response = await operations.AllocateForLanding(request);
                Console.WriteLine($"Result:{response.Status.ToString()}");
            } while (true);
        }
    }
}