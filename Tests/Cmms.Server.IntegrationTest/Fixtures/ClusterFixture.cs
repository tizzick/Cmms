namespace Cmms.Server.IntegrationTest.Fixtures;

using System.Threading.Tasks;
using Orleans.TestingHost;
using Xunit;
using Xunit.Abstractions;

/// <summary>
/// 
/// </summary>
/// <param name="testOutputHelper"></param>
public class ClusterFixture(ITestOutputHelper testOutputHelper) : IAsyncLifetime
{
    /// <summary>
    /// 
    /// </summary>
    public TestCluster Cluster { get; } = new TestClusterBuilder()
            .AddClientBuilderConfigurator<TestClientBuilderConfigurator>()
            .AddSiloBuilderConfigurator<TestSiloConfigurator>()
            .Build();

    /// <summary>
    /// 
    /// </summary>
    public ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task DisposeAsync() => this.Cluster.DisposeAsync().AsTask();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task InitializeAsync() => this.Cluster.DeployAsync();
}
