namespace Cmms.Api.Assets;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
//using System.Net.Mime;
//using System.Collections.ObjectModel;
using Cmms.Api.Common.Constants;
using Cmms.Api.Common.ViewModels;

/// <summary>
/// The assets controller
/// </summary>
[Route("[controller]")]
[ApiController]
[ApiVersion(ApiVersionName.V1)]
public class AssetController : ControllerBase
{

    /// <summary>
    /// Returns an Allow HTTP header with the allowed HTTP methods.
    /// </summary>
    /// <returns>A 200 OK response.</returns>
    [HttpOptions(Name = AssetsControllerRoute.OptionsAssets)]
    public IActionResult Options()
    {
        this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
            HeaderNames.Allow,
            HttpMethods.Get,
            HttpMethods.Head,
            HttpMethods.Options,
            HttpMethods.Post);
        return this.Ok();
    }


    /// <summary>
    /// Gets the asset with the specified unique identifier.
    /// </summary>
    /// <param name="command">The action command.</param>
    /// <param name="assetId">The assets unique identifier.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
    /// <returns>A 200 OK response containing the asset or a 404 Not Found if a asset with the specified unique
    /// identifier was not found.</returns>
    [HttpGet("{assetId}", Name = AssetsControllerRoute.GetAsset)]
    [HttpHead("{assetId}", Name = AssetsControllerRoute.HeadAsset)]
    public Task<IActionResult> GetAsync(
        [FromServices] GetAssetCommand command,
        Guid assetId,
        CancellationToken cancellationToken) => command.ExecuteAsync(assetId, cancellationToken);


    /// <summary>
    /// Gets a collection of assets.
    /// </summary>
    /// <param name="command">The action command.</param>
    /// <param name="pageOptions">The page options.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
    /// <returns>A 200 OK response containing a collection of assets, a 400 Bad Request if the page request
    /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
    /// </returns>
    [HttpGet("", Name = AssetsControllerRoute.GetAssetPage)]
    [HttpHead("", Name = AssetsControllerRoute.HeadAssetPage)]
    public Task<IActionResult> GetPageAsync(
        [FromServices] GetAssetPageCommand command,
        [FromQuery] PageOptions pageOptions,
        CancellationToken cancellationToken) => command.ExecuteAsync(pageOptions, cancellationToken);
}
