<img src="assets/icon.png" width="100" />

# NServiceBus.Extensions.EndpointStarted

Enables to register a callback to be notified when the NServiceBus endpoints are started:

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
    var builder = Host.CreateApplicationBuilder(args);

    var endpointConfiguration = new EndpointConfiguration("SampleEndpoint");
    endpointConfiguration.UseTransport<A-Transport>();
    endpointConfiguration.OnEndpointStarted(session =>
    {
        return Task.CompletedTask;
    });

    builder.Services.AddNServiceBusEndpoint(endpointConfiguration);

    builder.Build().Run();
}
```

When using generic hosting support it might be needed to send messages, or perform other operations, upon endpoint startup. The `OnEndpointStarted` is designed to invoke the provided callback when the endpoint is started.

## How to install

The package is available on Nuget as [NServiceBus.Extensions.EndpointStarted](https://www.nuget.org/packages/NServiceBus.Extensions.EndpointStarted/)

## Compatibility

| NServiceBus.Extensions.EndpointStarted | NServiceBus | .NET    |
|----------------------------------------|-------------|---------|
| 4.x                                    | 10.x        | .NET 10 |
| 3.x                                    | 9.x         | .NET 8  |

---

Icon [Call Back](https://thenounproject.com/search/?q=callback&i=1236265) by Lakshisha from the Noun Project
