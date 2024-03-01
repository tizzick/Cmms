namespace Cmms.Api.Common.ConfigureOptions;

using Microsoft.Extensions.Options;

/// <summary>
/// Configures custom routing settings which determines how URL's are generated.
/// </summary>
public class ConfigureRouteOptions : IConfigureOptions<RouteOptions>
{
    /// <summary>
    /// Configures custom routing settings which determines how URL's are generated.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(RouteOptions options) => options.LowercaseUrls = true;
}
