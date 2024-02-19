namespace Cmms.Server.IntegrationTest.Fixtures;
using Orleans.Hosting;
using Orleans.TestingHost;
using Cmms.Abstractions.Constants;

/// <summary>
/// 
/// </summary>
public class TestSiloConfigurator : ISiloConfigurator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="siloBuilder"></param>
    public void Configure(ISiloBuilder siloBuilder) =>
        siloBuilder
            .AddMemoryGrainStorageAsDefault()
            .AddMemoryGrainStorage("PubSubStore")
            .AddSimpleMessageStreamProvider(StreamProviderName.Default);
}
