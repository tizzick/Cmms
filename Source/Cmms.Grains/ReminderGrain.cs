namespace Cmms.Grains;

using Orleans;
using Orleans.Runtime;
using Cmms.Abstractions.Constants;
using Cmms.Abstractions.Grains;

/// <summary>
/// 
/// </summary>
public class ReminderGrain : Grain, IReminderGrain, IRemindable
{
    private const string ReminderName = "SomeReminder";
    private string? reminder;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reminder"></param>
    /// <returns></returns>
    public ValueTask SetReminderAsync(string reminder)
    {
        this.reminder = reminder;
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task OnActivateAsync()
    {
        // Reminders are timers that are persisted to storage, so they are resilient if the node goes down. They
        // should not be used for high-frequency timers their period should be measured in minutes, hours or days.
        await this.RegisterOrUpdateReminder(
                ReminderName,
                TimeSpan.FromSeconds(150),
                TimeSpan.FromSeconds(60))
            .ConfigureAwait(true);
        await base.OnActivateAsync().ConfigureAwait(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reminderName"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        if (string.Equals(ReminderName, reminderName, StringComparison.Ordinal))
        {
            if (!string.IsNullOrEmpty(this.reminder))
            {
                await this.PublishReminderAsync(this.reminder).ConfigureAwait(true);
            }
        }
    }

    private Task PublishReminderAsync(string reminder)
    {
        var streamProvider = this.GetStreamProvider(StreamProviderName.Default);
        var stream = streamProvider.GetStream<string>(Guid.Empty, StreamName.Reminder);
        return stream.OnNextAsync(reminder);
    }
}
