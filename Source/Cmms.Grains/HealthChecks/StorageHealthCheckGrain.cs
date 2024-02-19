namespace Cmms.Grains.HealthChecks;

using Orleans;
using Orleans.Placement;
using Orleans.Runtime;
using Cmms.Abstractions.Grains.HealthChecks;

/// <summary>
/// 
/// </summary>
[PreferLocalPlacement]
public class StorageHealthCheckGrain : Grain<Guid>, IStorageHealthCheckGrain
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask CheckAsync()
    {
        try
        {
            this.State = Guid.NewGuid();
            await this.WriteStateAsync().ConfigureAwait(true);
            await this.ReadStateAsync().ConfigureAwait(true);
            await this.ClearStateAsync().ConfigureAwait(true);
        }
        finally
        {
            this.DeactivateOnIdle();
        }
    }
}
