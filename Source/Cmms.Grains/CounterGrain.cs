namespace Cmms.Grains;

using Orleans;
using Cmms.Abstractions.Grains;

/// <summary>
/// 
/// </summary>
public class CounterGrain : Grain<long>, ICounterGrain
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public async ValueTask<long> AddCountAsync(long value)
    {
        this.State += value;
        await this.WriteStateAsync().ConfigureAwait(true);
        return this.State;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask<long> GetCountAsync() => ValueTask.FromResult(this.State);
}
