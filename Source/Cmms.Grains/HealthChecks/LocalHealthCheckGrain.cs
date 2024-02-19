namespace Cmms.Grains;

using Orleans;
using Orleans.Concurrency;
using Cmms.Abstractions.Grains.HealthChecks;

/// <summary>
/// 
/// </summary>
[StatelessWorker(1)]
public class LocalHealthCheckGrain : Grain, ILocalHealthCheckGrain
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask CheckAsync() => ValueTask.CompletedTask;
}
