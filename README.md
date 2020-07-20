<img src="assets/icon.png" width="100" />

# NServiceBus.Extensions.EndpointStarted

Enables to register a callback to be notified when the NServiceBus endpoints is started:

```csharp
var endpointConfiguration = new EndpointConfiguration("SampleEndpoint");
endpointConfiguration.UseTransport<LearningTransport>();
endpointConfiguration.OnEndpointStarted(session =>
{
    return Task.CompletedTask;
});
```

The endpoint started callback becomes quite useful when used in combination with generic hosting support:

```csharp
public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
}

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("SampleEndpoint");
            endpointConfiguration.UseTransport<A-Transport>();
            endpointConfiguration.OnEndpointStarted(session =>
            {
                return Task.CompletedTask;
            });

            return endpointConfiguration;
        })
        .ConfigureLogging((hostingContext, loggingBuilder) =>
        {
            loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        });
```

When using generic hosting support it might be needed to send messages, or perform other operations, upon endpoint startup. The `OnEndpointStarted` is designed to invoke the provided callback when the endpoint is started.

---

Icon [Call Back](https://thenounproject.com/search/?q=callback&i=1236265) by Lakshisha from the Noun Project