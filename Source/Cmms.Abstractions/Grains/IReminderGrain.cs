namespace Cmms.Abstractions.Grains;

using Orleans;

/// <summary>
/// 
/// </summary>
public interface IReminderGrain : IGrainWithGuidKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reminder"></param>
    /// <returns></returns>
    ValueTask SetReminderAsync(string reminder);
}
