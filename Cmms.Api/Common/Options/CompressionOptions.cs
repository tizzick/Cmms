namespace Cmms.Api.Common.Options;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// The dynamic response compression options for the application.
/// </summary>
public class CompressionOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompressionOptions"/> class.
    /// </summary>
    public CompressionOptions() => this.MimeTypes = [];

    /// <summary>
    /// Gets a list of MIME types to be compressed in addition to the default set used by ASP.NET Core.
    /// </summary>
    [Required]
    public List<string> MimeTypes { get; }
}
