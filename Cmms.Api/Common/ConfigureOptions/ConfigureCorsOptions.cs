namespace Cmms.Api.Common.ConfigureOptions;

using Cmms.Api.Common.Constants;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

/// <summary>
/// Configures cross-origin resource sharing (CORS) policies.
/// See https://docs.asp.net/en/latest/security/cors.html.
/// </summary>
public class ConfigureCorsOptions : IConfigureOptions<CorsOptions>
{
    /// <summary>
    /// Configures a CORS policy which allows any origin, header, and method.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(CorsOptions options) =>
        // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
        // or a [EnableCors("PolicyName")] attribute on your controller or action.
        options.AddPolicy(
            CorsPolicyName.AllowAny,
            x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
}
