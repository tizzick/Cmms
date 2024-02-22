namespace Cmms.Api.Assets;

/// <summary>
/// Interface for asset repository
/// </summary>
public interface IAssetRepository
{
    /// <summary>
    /// Get an asset by its id
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Asset?> GetAsync(Guid assetId, CancellationToken cancellationToken);

    /// <summary>
    /// Get a list of assets
    /// </summary>
    /// <param name="first"></param>
    /// <param name="createdAfter"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Asset>> GetAssetsAsync(
        int? first,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get the total count of assets
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get the total count of assets
    /// </summary>
    /// <param name="first"></param>
    /// <param name="createdAfter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> GetHasNextPageAsync(
        int? first,
        DateTimeOffset? createdAfter,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get a list of assets in reverse order
    /// </summary>
    /// <param name="last"></param>
    /// <param name="createdAfter"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Asset>> GetAssetsReverseAsync(
        int? last,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get the total count of assets in reverse order
    /// </summary>
    /// <param name="last"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> GetHasPreviousPageAsync(
        int? last,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken);
}
