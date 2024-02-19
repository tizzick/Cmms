namespace Cmms.Grains;

using Orleans;
using Cmms.Abstractions.Constants;
using Cmms.Abstractions.Grains;

/// <summary>
/// 
/// </summary>
public class HelloGrain : Grain, IHelloGrain
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async ValueTask<string> SayHelloAsync(string name)
    {
        await this.IncrementCounterAsync().ConfigureAwait(true);
        await this.PublishSaidHelloAsync(name).ConfigureAwait(true);

        return $"Hello {name}!";
    }

    private ValueTask IncrementCounterAsync()
    {
        var counter = this.GrainFactory.GetGrain<ICounterStatelessGrain>(0L);
        return counter.IncrementAsync();
    }

    private Task PublishSaidHelloAsync(string name)
    {
        var streamProvider = this.GetStreamProvider(StreamProviderName.Default);
        var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
        return stream.OnNextAsync(name);
    }
}
