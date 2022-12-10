using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus.Features;
using NServiceBus.ObjectBuilder;

namespace NServiceBus
{
    class EndpointStartupCallback : Feature
    {
        protected override void Setup(FeatureConfigurationContext context)
        {
            var callback = context.Settings.Get<Func<IMessageSession, IServiceProvider, Task>>(OnEndpointStartedEndpointConfigurationExtensions.NServiceBusExtensionsEndpointStartedSetting);
            context.RegisterStartupTask(b => new CallbackStartupTask(callback, b));
        }
    }

    class CallbackStartupTask : FeatureStartupTask
    {
        private readonly Func<IMessageSession, IServiceProvider, Task> _callback;
        private readonly IServiceProvider _builder;

        public CallbackStartupTask(Func<IMessageSession, IServiceProvider, Task> callback, IServiceProvider builder)
        {
            _callback = callback;
            _builder = builder;
        }

        protected override Task OnStart(IMessageSession session, CancellationToken token)
        {
            return _callback(session, _builder);
        }

        protected override Task OnStop(IMessageSession session, CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}