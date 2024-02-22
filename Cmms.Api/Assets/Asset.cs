namespace Cmms.Api.Assets;


/// <summary>
/// 
/// </summary>
public class Asset
{
    /// <summary>
    /// The id of the asset
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Date the asset was last modified
    /// </summary>
    public DateTimeOffset Modified { get; set; }
    /// <summary>
    /// The name of the asset
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// The date the asset was created
    /// </summary>
    public DateTimeOffset Created { get; set; }
}
