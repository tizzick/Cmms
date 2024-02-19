namespace Cmms.Server.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;
using Cmms.Abstractions.Grains.HealthChecks;

/// <summary>
/// Verifies connectivity to a <see cref="ILocalHealthCheckGrain"/> activation. As this grain is a
/// stateless worker, validation always occurs in the silo where the health check is issued.
/// </summary>
public class GrainHealthCheck(IClusterClient client, ILogger<GrainHealthCheck> logger) : IHealthCheck
{
    private const string FailedMessage = "Failed local health check.";
    private readonly IClusterClient client = client;
    private readonly ILogger<GrainHealthCheck> logger = logger;

    /// <summary>
    /// Verifies connectivity to a <see cref="ILocalHealthCheckGrain"/> activation.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await this.client.GetGrain<ILocalHealthCheckGrain>(Guid.Empty).CheckAsync().ConfigureAwait(false);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            this.logger.FailedLocalHealthCheck(exception);
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            return HealthCheckResult.Unhealthy(FailedMessage, exception);
        }

        return HealthCheckResult.Healthy();
    }
}
