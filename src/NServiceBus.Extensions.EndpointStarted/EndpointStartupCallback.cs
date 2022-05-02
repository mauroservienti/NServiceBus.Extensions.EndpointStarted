using System;
using System.Threading.Tasks;
using NServiceBus.Features;
using NServiceBus.ObjectBuilder;

namespace NServiceBus
{
    class EndpointStartupCallback : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {
            var callback = context.Settings.Get<Func<IMessageSession, IBuilder, Task>>(OnEndpointStartedEndpointConfigurationExtensions.NServiceBusExtensionsEndpointStartedSetting);
            context.RegisterStartupTask(b => new CallbackStartupTask(callback, b));
        }
    }

    class CallbackStartupTask : FeatureStartupTask
    {
        private readonly Func<IMessageSession, IBuilder, Task> _callback;
        private readonly IBuilder _builder;

        public CallbackStartupTask(Func<IMessageSession, IBuilder, Task> callback, IBuilder builder)
        {
            _callback = callback;
            _builder = builder;
        }

        protected override Task OnStart(IMessageSession session)
        {
            return _callback(session, _builder);
        }

        protected override Task OnStop(IMessageSession session)
        {
            return Task.CompletedTask;
        }
    }
}