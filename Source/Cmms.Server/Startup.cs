namespace Cmms.Server;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Cmms.Server.HealthChecks;

#pragma warning disable CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
/// <summary>
/// Initializes a new instance of the <see cref="Startup"/> class.
/// </summary>
/// <param name="configuration">The application configuration, where key value pair settings are stored (See
/// http://docs.asp.net/en/latest/fundamentals/configuration.html).</param>
/// <param name="webHostEnvironment">The environment the application is running under. This can be Development,
/// Staging or Production by default (See http://docs.asp.net/en/latest/fundamentals/environments.html).</param>
public class Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
#pragma warning restore CA1724 // The type name conflicts with the namespace name 'Orleans.Runtime.Startup'
{
    private readonly IConfiguration configuration = configuration;
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;

    /// <summary>
    /// Configures the application's services.
    /// </summary>
    /// <param name="services"></param>
    public virtual void ConfigureServices(IServiceCollection services) =>
        services
            .AddRouting(options => options.LowercaseUrls = true)
            .AddHealthChecks()
            .AddCheck<ClusterHealthCheck>(nameof(ClusterHealthCheck))
            .AddCheck<GrainHealthCheck>(nameof(GrainHealthCheck))
            .AddCheck<SiloHealthCheck>(nameof(SiloHealthCheck))
            .AddCheck<StorageHealthCheck>(nameof(StorageHealthCheck));

    /// <summary>
    /// Configures the application's request pipeline.
    /// </summary>
    /// <param name="application"></param>
    public virtual void Configure(IApplicationBuilder application) =>
        application
            .UseRouting()
            .UseEndpoints(
                builder =>
                {
                    builder.MapHealthChecks("/status");
                    builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
                });
}
