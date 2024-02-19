namespace Cmms.Abstractions.Grains.HealthChecks;

using Orleans;

/// <summary>
/// 
/// </summary>
public interface ILocalHealthCheckGrain : IGrainWithGuidKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask CheckAsync();
}
