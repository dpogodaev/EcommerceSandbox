using EcommerceSandbox.WebMvc.Adapters;
using EcommerceSandbox.WebMvc.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.WebMvc"/>.
/// </summary>
public static class WebMvcConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.WebMvc"/>. 
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddWebMvcConfig(this IServiceCollection services)
    {
        services.AddMvc(x => x.EnableEndpointRouting = false);

        AddServices(services);
    }

    #region Private methods

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductServiceAdapter>();
    }

    #endregion
}