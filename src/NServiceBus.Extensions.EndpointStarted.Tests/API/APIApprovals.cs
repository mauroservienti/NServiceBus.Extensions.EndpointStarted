using System.Runtime.CompilerServices;
using NUnit.Framework;
using PublicApiGenerator;

namespace NServiceBus.Extensions.EndpointStarted.Tests.API
{
    public class APIApprovals
    {
        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Approve_API()
        {
            var publicApi = typeof(OnEndpointStartedEndpointConfigurationExtensions).Assembly.GeneratePublicApi(new ApiGeneratorOptions
            {
                ExcludeAttributes = new[]
                {
                    "System.Runtime.Versioning.TargetFrameworkAttribute"
                }
            });
            Approver.Verify(publicApi.Replace(".git", ""));
        }
    }
}