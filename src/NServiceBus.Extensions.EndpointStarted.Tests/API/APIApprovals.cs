using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using PublicApiGenerator;
using VerifyNUnit;

namespace NServiceBus.Extensions.EndpointStarted.Tests.API
{
    public class APIApprovals
    {
        [Test]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Approve_API()
        {
            var publicApi = typeof(OnEndpointStartedEndpointConfigurationExtensions).Assembly.GeneratePublicApi(new ApiGeneratorOptions
            {
                ExcludeAttributes = new[]
                {
                    "System.Runtime.Versioning.TargetFrameworkAttribute"
                }
            });
            return Verifier.Verify(publicApi);
        }
    }
}