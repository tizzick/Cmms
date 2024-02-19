namespace Cmms.Server.Options;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Orleans.Configuration;
/// <summary>
/// 
/// </summary>
public class ApplicationOptions
{
    /// <summary>
    /// 
    /// </summary>
    public ClusterOptions Cluster { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public KestrelServerOptions Kestrel { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public StorageOptions Storage { get; set; } = default!;
}
