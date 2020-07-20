using System;
using System.Threading.Tasks;
using NServiceBus.Configuration.AdvancedExtensibility;

namespace NServiceBus
{
    public static class OnEndpointStartedEndpointConfigurationExtensions
    {
        internal const string NServiceBusExtensionsEndpointStartedSetting = "NServiceBus.Extensions.EndpointStarted";

        public static void OnEndpointStarted(this EndpointConfiguration configuration, Func<IMessageSession, Task> onEndpointStarted)
        {
            if (onEndpointStarted == null)
            {
                throw new ArgumentNullException(nameof(onEndpointStarted));
            }

            var settings = configuration.GetSettings();
            settings.Set(NServiceBusExtensionsEndpointStartedSetting, onEndpointStarted);
            configuration.EnableFeature<EndpointStartupCallback>();
        }
    }
}