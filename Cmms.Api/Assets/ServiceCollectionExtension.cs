namespace Cmms.Api.Assets;


internal static class ServiceCollectionExtension
{
    public static IServiceCollection AddAssetServices(this IServiceCollection services) =>
        services
            .AddSingleton<GetAssetCommand>()
            .AddSingleton<GetAssetPageCommand>()
        .AddSingleton<IAssetRepository, AssetRepository>();

}
