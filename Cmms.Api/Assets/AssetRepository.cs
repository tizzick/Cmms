namespace Cmms.Api.Assets;

using Cmms.Api.Common.Extensions;

/// <summary>
/// Dummy repository for testing, tobe replaced with real repository later
/// </summary>
public class AssetRepository : IAssetRepository
{
    private static readonly List<Asset> Assets =
    [
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-8),
            Name = "Lamborghini",
            Modified = DateTimeOffset.UtcNow.AddDays(-8),
        },
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-5),
            Name = "Mazda",
            Modified = DateTimeOffset.UtcNow.AddDays(-6),
        },
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-10),
            Name = "Honda",
            Modified = DateTimeOffset.UtcNow.AddDays(-3),
        },
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-3),
            Name = "Lotus",
            Modified = DateTimeOffset.UtcNow.AddDays(-3),
        },
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-12),
            Name = "Mitsubishi",
            Modified = DateTimeOffset.UtcNow.AddDays(-2),
        },
        new Asset()
        {
            Id = new Guid(),
            Created = DateTimeOffset.UtcNow.AddDays(-1),
            Name = "McLaren",
            Modified = DateTimeOffset.UtcNow.AddDays(-1),
        },
    ];


    /// <summary>
    /// Get an asset by its id
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Asset?> GetAsync(Guid assetId, CancellationToken cancellationToken)
    {
        var asset = Assets.FirstOrDefault(x => x.Id == assetId);
        return Task.FromResult(asset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="first"></param>
    /// <param name="createdAfter"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Asset>> GetAssetsAsync(
        int? first,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Assets
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(first.HasValue, x => x.Take(first!.Value))
            .ToList());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="last"></param>
    /// <param name="createdAfter"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Asset>> GetAssetsReverseAsync(
        int? last,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Assets
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(last.HasValue, x => x.TakeLast(last!.Value))
            .ToList());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="first"></param>
    /// <param name="createdAfter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> GetHasNextPageAsync(
        int? first,
        DateTimeOffset? createdAfter,
        CancellationToken cancellationToken) =>
        Task.FromResult(Assets
            .OrderBy(x => x.Created)
            .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter!.Value))
            .If(first.HasValue, x => x.Skip(first!.Value))
            .Any());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="last"></param>
    /// <param name="createdBefore"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> GetHasPreviousPageAsync(
        int? last,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken) =>
        Task.FromResult(Assets
            .OrderBy(x => x.Created)
            .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore!.Value))
            .If(last.HasValue, x => x.SkipLast(last!.Value))
            .Any());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) => Task.FromResult(Assets.Count);

}
