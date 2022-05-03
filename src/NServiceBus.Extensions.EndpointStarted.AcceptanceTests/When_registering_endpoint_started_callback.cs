using System.Threading.Tasks;
using NServiceBus.AcceptanceTesting;
using NServiceBus.Extensions.EndpointStarted.AcceptanceTests.Config;
using NUnit.Framework;

namespace NServiceBus.Extensions.EndpointStarted.AcceptanceTests
{
    [TestFixture]
    public class When_registering_endpoint_started
    {
        private static bool callbackInvoked;

        [Test]
        public async Task Callback_is_invoked_as_expected()
        {
            var context = await Scenario.Define<Context>()
                .WithEndpoint<SenderEndpoint>()
                .Done(c => callbackInvoked)
                .Run();
        }

        class Context : ScenarioContext
        {

        }

        class SenderEndpoint : EndpointConfigurationBuilder
        {
            public SenderEndpoint()
            {
                EndpointSetup<DefaultServer>(config =>
                {
                    config.OnEndpointStarted(messageSession =>
                    {
                        callbackInvoked = true;
                        return Task.CompletedTask;
                    });
                });
            }
        }
    }
}