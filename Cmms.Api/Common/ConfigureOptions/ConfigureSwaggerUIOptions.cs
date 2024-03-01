namespace Cmms.Api.Common.ConfigureOptions;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

/// <summary>
/// Configures the Swagger UI options.
/// </summary>
/// <param name="apiVersionDescriptionProvider"></param>
public class ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider = apiVersionDescriptionProvider;

    /// <summary>
    /// Configures the Swagger UI options.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerUIOptions options)
    {
        // Set the Swagger UI browser document title.
        options.DocumentTitle = AssemblyInformation.Current.Product;
        // Set the Swagger UI to render at '/'.
        options.RoutePrefix = string.Empty;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

        foreach (var apiVersionDescription in this.apiVersionDescriptionProvider
            .ApiVersionDescriptions
            .OrderByDescending(x => x.ApiVersion))
        {
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}");
        }
    }
}
