namespace Cmms.Abstractions.Grains;

using Orleans;

/// <summary>
/// 
/// </summary>
public interface IHelloGrain : IGrainWithGuidKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    ValueTask<string> SayHelloAsync(string name);
}
