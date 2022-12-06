using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Common;

/// <summary>
/// AutoMapper configuration.
/// </summary>
public static class AutoMapperConfig
{
    /// <summary>
    /// Adds the object mapper configuration for <c>AutoMapper</c> library.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddAutoMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(x =>
            x.AddMaps(
                "EcommerceSandbox.DomainEntities",
                "EcommerceSandbox.DomainServices",
                "EcommerceSandbox.AppServices",
                "EcommerceSandbox.AppStorages",
                "EcommerceSandbox.Common",
                "EcommerceSandbox.EfCore",
                "EcommerceSandbox.EfCoreMssql",
                "EcommerceSandbox.WebMvc")
        );
    }
}