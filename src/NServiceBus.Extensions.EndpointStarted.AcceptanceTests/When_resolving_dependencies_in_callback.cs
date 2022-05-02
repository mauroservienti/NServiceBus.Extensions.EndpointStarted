using System.Threading.Tasks;
using NServiceBus.AcceptanceTesting;
using NServiceBus.Extensions.EndpointStarted.AcceptanceTests.Config;
using NUnit.Framework;

namespace NServiceBus.Extensions.EndpointStarted.AcceptanceTests;

[TestFixture]
public class When_resolving_dependencies_in_callback
{
    [Test]
    public async Task Should_resolve_registered_services()
    {
        var context = await Scenario.Define<Context>()
            .WithEndpoint<EndpointWithDependencies>()
            .Done(c => c.EndpointsStarted)
            .Run();

        Assert.NotNull(context.ResolvedService);
    }

    class Context : ScenarioContext
    {
        public SingletonService ResolvedService { get; set; }
    }

    class EndpointWithDependencies : EndpointConfigurationBuilder
    {
        public EndpointWithDependencies() => EndpointSetup<DefaultServer>((configuration, r) =>
        {
            configuration.RegisterComponents(c =>
            {
                c.ConfigureComponent<SingletonService>(DependencyLifecycle.SingleInstance);
            });
            configuration.OnEndpointStarted((session, builder) =>
            {
                var testContext = r.ScenarioContext as Context;
                testContext.ResolvedService = builder.Build<SingletonService>();
                return Task.CompletedTask;
            });
        });
    }

    class SingletonService
    {
    }
}