using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace SampleUsingGenericHosting
{
    class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var endpointConfiguration = new EndpointConfiguration("SampleEndpoint");
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.OnEndpointStarted(session =>
            {
                return Task.CompletedTask;
            });

            builder.Services.AddNServiceBusEndpoint(endpointConfiguration);

            builder.Build().Run();
        }
    }
}
