namespace Cmms.Abstractions.Grains.HealthChecks;

using Orleans;

/// <summary>
/// 
/// </summary>
public interface IStorageHealthCheckGrain : IGrainWithGuidKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask CheckAsync();
}
