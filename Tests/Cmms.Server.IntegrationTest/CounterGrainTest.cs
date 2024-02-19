namespace Cmms.Server.IntegrationTest;

using System;
using System.Threading.Tasks;
using Cmms.Abstractions.Grains;
using Cmms.Server.IntegrationTest.Fixtures;
using Xunit;
using Xunit.Abstractions;

/// <summary>
/// 
/// </summary>
/// <param name="testOutputHelper"></param>
public class CounterGrainTest(ITestOutputHelper testOutputHelper) : ClusterFixture(testOutputHelper)
{
    /// <summary>
    /// Add one to count and check value
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task AddCount_PassValue_ReturnsTotalCountAsync()
    {
        var grain = this.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

        var count = await grain.AddCountAsync(10L).ConfigureAwait(false);

        Assert.Equal(10L, count);
    }

    /// <summary>
    /// Get count and check value
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetCount_Default_ReturnsTotalCountAsync()
    {
        var grain = this.Cluster.GrainFactory.GetGrain<ICounterGrain>(Guid.Empty);

        var count = await grain.GetCountAsync().ConfigureAwait(false);

        Assert.Equal(0L, count);
    }
}
