namespace Cmms.Api.Common.Options;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.Kestrel.Core;

/// <summary>
/// All options for the application.
/// </summary>
public class ApplicationOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationOptions"/> class.
    /// </summary>
    public ApplicationOptions() => this.CacheProfiles = [];

    /// <summary>
    /// Gets or sets the cache profile options.
    /// </summary>
    [Required]
    public CacheProfileOptions CacheProfiles { get; }

    /// <summary>
    /// Gets or sets the compression options.
    /// </summary>
    [Required]
    public CompressionOptions Compression { get; set; } = default!;

    /// <summary>
    /// Gets or sets the forward headers options.
    /// </summary>
    [Required]
    public ForwardedHeadersOptions ForwardedHeaders { get; set; } = default!;

    /// <summary>
    /// Gets or sets the host options.
    /// </summary>
    [Required]
    public HostOptions Host { get; set; } = default!;

    /// <summary>
    /// Gets or sets the Kestrel options.
    /// </summary>
    [Required]
    public KestrelServerOptions Kestrel { get; set; } = default!;
}
