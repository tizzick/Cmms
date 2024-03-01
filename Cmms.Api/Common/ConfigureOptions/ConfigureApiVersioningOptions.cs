namespace Cmms.Api.Common.ConfigureOptions;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

/// <summary>
/// Configures API versioning options.
/// </summary>
public class ConfigureApiVersioningOptions :
    IConfigureOptions<ApiVersioningOptions>,
    IConfigureOptions<ApiExplorerOptions>
{
    /// <summary>
    /// Configures the <see cref="ApiVersioningOptions"/>.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(ApiVersioningOptions options)
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    }


    /// <summary>
    /// Configures the <see cref="ApiExplorerOptions"/>.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(ApiExplorerOptions options) =>
        // Version format: 'v'major[.minor][-status]
        options.GroupNameFormat = "'v'VVV";
}
