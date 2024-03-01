namespace Cmms.Api.Common.ConfigureOptions;

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using Cmms.Api.Common.ViewModels;

/// <summary>
/// Configures the JSON options for the application.
/// </summary>
public class ConfigureJsonOptions(IWebHostEnvironment webHostEnvironment) : IConfigureOptions<JsonOptions>
{
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;

    /// <summary>
    /// Configures the JSON options for the application.
    /// </summary>
    /// <param name="options"></param>
    public void Configure(JsonOptions options)
    {
        var jsonSerializerOptions = options.JsonSerializerOptions;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        // Pretty print the JSON in development for easier debugging.
        jsonSerializerOptions.WriteIndented = this.webHostEnvironment.IsDevelopment() ||
            this.webHostEnvironment.IsEnvironment(Constants.EnvironmentName.Test);

        //jsonSerializerOptions.AddContext<CustomJsonSerializerContext>();
    }
}
