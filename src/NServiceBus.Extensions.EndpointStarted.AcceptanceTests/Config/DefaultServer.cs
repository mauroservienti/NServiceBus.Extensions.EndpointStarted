﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NServiceBus.AcceptanceTesting.Customization;
using NServiceBus.AcceptanceTesting.Support;
using NServiceBus.Features;

namespace NServiceBus.Extensions.EndpointStarted.AcceptanceTests.Config
{
    public class DefaultServer : IEndpointSetupTemplate
    {
        public Task<EndpointConfiguration> GetConfiguration(RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization)
        {
            var types = endpointConfiguration.GetTypesScopedByTestClass();
            var typesToInclude= new List<Type>(types);

            var configuration = new EndpointConfiguration(endpointConfiguration.EndpointName);

            configuration.TypesToIncludeInScan(typesToInclude);
            configuration.EnableInstallers();

            configuration.UseContainer(new AcceptanceTestingContainer());
            configuration.DisableFeature<TimeoutManager>();

            var recoverability = configuration.Recoverability();
            recoverability.Delayed(delayed => delayed.NumberOfRetries(0));
            recoverability.Immediate(immediate => immediate.NumberOfRetries(0));
            configuration.SendFailedMessagesTo("error");

            var storageDir = StorageUtils.GetAcceptanceTestingTransportStorageDirectory();

            var transportConfig = configuration.UseTransport<AcceptanceTestingTransport>()
                .StorageDirectory(storageDir);

            configuration.RegisterComponentsAndInheritanceHierarchy(runDescriptor);

            configurationBuilderCustomization(configuration);

            runDescriptor.OnTestCompleted(summary =>
            {
                if (Directory.Exists(storageDir))
                {
                    Directory.Delete(storageDir, true);
                }

                return Task.FromResult(0);
            });

            return Task.FromResult(configuration);
        }
    }
}