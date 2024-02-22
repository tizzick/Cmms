namespace Cmms.Api.Assets;

using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

/// <summary>
/// Command to get an asset from the repository
/// </summary>
/// <param name="actionContextAccessor"></param>
/// <param name="assetRepository"></param>
public class GetAssetCommand(IActionContextAccessor actionContextAccessor, IAssetRepository assetRepository)
{
    private readonly IActionContextAccessor actionContextAccessor = actionContextAccessor;
    private readonly IAssetRepository assetRepository = assetRepository;


    /// <summary>
    /// Returns and ok response containing the asset with the specified unique identifier or a not found response if
    /// assetId was not found.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IActionResult> ExecuteAsync(Guid assetId, CancellationToken cancellationToken)
    {
        var asset = await this.assetRepository.GetAsync(assetId, cancellationToken).ConfigureAwait(false);
        if (asset is null)
        {
            return new NotFoundResult();
        }

        var httpContext = this.actionContextAccessor.ActionContext!.HttpContext;
        var ifModifiedSince = httpContext.Request.Headers.IfModifiedSince;
        if (ifModifiedSince.Count > 0 &&
            DateTimeOffset.TryParse(ifModifiedSince, out var ifModifiedSinceDateTime) &&
            (ifModifiedSinceDateTime >= asset.Modified))
        {
            return new StatusCodeResult(StatusCodes.Status304NotModified);
        }

        //var carViewModel = this.carMapper.Map(car);
        httpContext.Response.Headers.LastModified = asset.Modified.ToString("R", CultureInfo.InvariantCulture);
        return new OkObjectResult(asset);
    }
}
