using Soenneker.Tests.HostedUnit;

namespace Soenneker.Attio.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AttioOpenApiClientTests : HostedUnitTest
{
    public AttioOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
