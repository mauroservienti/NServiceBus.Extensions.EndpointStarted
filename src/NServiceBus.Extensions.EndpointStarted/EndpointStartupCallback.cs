using System;
using System.Threading.Tasks;
using NServiceBus.Features;

namespace NServiceBus
{
    class EndpointStartupCallback : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {
            var callback = context.Settings.Get<Func<IMessageSession, Task>>(OnEndpointStartedEndpointConfigurationExtensions.NServiceBusExtensionsEndpointStartedSetting);
            context.RegisterStartupTask(new CallbackStartupTask(callback));
        }
    }

    class CallbackStartupTask : FeatureStartupTask
    {
        private readonly Func<IMessageSession, Task> _callback;

        public CallbackStartupTask(Func<IMessageSession,Task> callback)
        {
            _callback = callback;
        }

        protected override Task OnStart(IMessageSession session)
        {
            return _callback(session);
        }

        protected override Task OnStop(IMessageSession session)
        {
            return Task.CompletedTask;
        }
    }
}