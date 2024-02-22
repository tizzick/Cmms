namespace Cmms.Api.Assets;

using Cmms.Api.Common.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Boxed.AspNetCore;
using Cmms.Api.Common.Constants;

/// <summary>
/// Command to get a page of assets from the repository
/// </summary>
/// <param name="actionContextAccessor"></param>
/// <param name="assetRepository"></param>
/// <param name="pageOptionsValidator"></param>
/// <param name="linkGenerator"></param>
public class GetAssetPageCommand(
        IActionContextAccessor actionContextAccessor,
        IAssetRepository assetRepository,
        IValidator<PageOptions> pageOptionsValidator,
        LinkGenerator linkGenerator)
{
    private const int DefaultPageSize = 3;

    private readonly IActionContextAccessor actionContextAccessor = actionContextAccessor;
    private readonly IAssetRepository assetRepository = assetRepository;
    private readonly IValidator<PageOptions> pageOptionsValidator = pageOptionsValidator;
    private readonly LinkGenerator linkGenerator = linkGenerator;

    /// <summary>
    /// Returns a page of assets from the repository
    /// </summary>
    /// <param name="pageOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IActionResult> ExecuteAsync(PageOptions pageOptions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(pageOptions);

        var validationResult = this.pageOptionsValidator.Validate(pageOptions);
        if (!validationResult.IsValid)
        {
            var modelState = this.actionContextAccessor.ActionContext!.ModelState;
            validationResult.AddToModelState(modelState, null);
            return new BadRequestObjectResult(new ValidationProblemDetails(modelState));
        }

        pageOptions.First = !pageOptions.First.HasValue && !pageOptions.Last.HasValue ? DefaultPageSize : pageOptions.First;
        var createdAfter = Cursor.FromCursor<DateTimeOffset?>(pageOptions.After);
        var createdBefore = Cursor.FromCursor<DateTimeOffset?>(pageOptions.Before);

        var getAssetsTask = this.GetAssetsAsync(pageOptions.First, pageOptions.Last, createdAfter, createdBefore, cancellationToken);
        var getHasNextPageTask = this.GetHasNextPageAsync(pageOptions.First, createdAfter, createdBefore, cancellationToken);
        var getHasPreviousPageTask = this.GetHasPreviousPageAsync(pageOptions.Last, createdAfter, createdBefore, cancellationToken);
        var totalCountTask = this.assetRepository.GetTotalCountAsync(cancellationToken);

        await Task.WhenAll(getAssetsTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask).ConfigureAwait(false);
        var assets = await getAssetsTask.ConfigureAwait(false);
        var hasNextPage = await getHasNextPageTask.ConfigureAwait(false);
        var hasPreviousPage = await getHasPreviousPageTask.ConfigureAwait(false);
        var totalCount = await totalCountTask.ConfigureAwait(false);

        if (assets is null)
        {
            return new NotFoundResult();
        }

        var (startCursor, endCursor) = Cursor.GetFirstAndLastCursor(assets, x => x.Created);
        //var assetViewModels = this.assetMapper.MapList(assets);

        var httpContext = this.actionContextAccessor.ActionContext!.HttpContext!;
        var connection = new Connection<Asset>()
        {
            PageInfo = new PageInfo()
            {
                Count = assets.Count,
                HasNextPage = hasNextPage,
                HasPreviousPage = hasPreviousPage,
                NextPageUrl = hasNextPage ? new Uri(this.linkGenerator.GetUriByRouteValues(
                    httpContext,
                    AssetsControllerRoute.GetAssetPage,
                    new PageOptions()
                    {
                        First = pageOptions.First ?? pageOptions.Last,
                        After = endCursor,
                    })!) : null,
                PreviousPageUrl = hasPreviousPage ? new Uri(this.linkGenerator.GetUriByRouteValues(
                    httpContext,
                    AssetsControllerRoute.GetAssetPage,
                    new PageOptions()
                    {
                        Last = pageOptions.First ?? pageOptions.Last,
                        Before = startCursor,
                    })!) : null,
                FirstPageUrl = new Uri(this.linkGenerator.GetUriByRouteValues(
                    httpContext,
                    AssetsControllerRoute.GetAssetPage,
                    new PageOptions()
                    {
                        First = pageOptions.First ?? pageOptions.Last,
                    })!),
                LastPageUrl = new Uri(this.linkGenerator.GetUriByRouteValues(
                    httpContext,
                    AssetsControllerRoute.GetAssetPage,
                    new PageOptions()
                    {
                        Last = pageOptions.First ?? pageOptions.Last,
                    })!),
            },
            TotalCount = totalCount,
        };
        connection.Items.AddRange(assets);

        httpContext.Response.Headers.Link = connection.PageInfo.ToLinkHttpHeaderValue();
        return new OkObjectResult(connection);
    }

    private Task<List<Asset>> GetAssetsAsync(
        int? first,
        int? last,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken)
    {
        Task<List<Asset>> getAssetsTask;
        if (first.HasValue)
        {
            getAssetsTask = this.assetRepository.GetAssetsAsync(first, createdAfter, createdBefore, cancellationToken);
        }
        else
        {
            getAssetsTask = this.assetRepository.GetAssetsReverseAsync(last, createdAfter, createdBefore, cancellationToken);
        }

        return getAssetsTask;
    }

    private async Task<bool> GetHasNextPageAsync(
        int? first,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken)
    {
        if (first.HasValue)
        {
            return await this.assetRepository
                .GetHasNextPageAsync(first, createdAfter, cancellationToken)
                .ConfigureAwait(false);
        }
        else if (createdBefore.HasValue)
        {
            return true;
        }

        return false;
    }

    private async Task<bool> GetHasPreviousPageAsync(
        int? last,
        DateTimeOffset? createdAfter,
        DateTimeOffset? createdBefore,
        CancellationToken cancellationToken)
    {
        if (last.HasValue)
        {
            return await this.assetRepository
                .GetHasPreviousPageAsync(last, createdBefore, cancellationToken)
                .ConfigureAwait(false);
        }
        else if (createdAfter.HasValue)
        {
            return true;
        }

        return false;
    }
}
