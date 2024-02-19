namespace Cmms.Abstractions.Grains;

using Orleans;

/// <summary>
/// Holds the total count.
/// </summary>
/// <remarks>Implemented using the 'Reduce' pattern (See https://github.com/OrleansContrib/DesignPatterns/blob/master/Reduce.md).</remarks>
/// <seealso cref="IGrainWithGuidKey" />
public interface ICounterGrain : IGrainWithGuidKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    ValueTask<long> AddCountAsync(long value);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<long> GetCountAsync();
}
