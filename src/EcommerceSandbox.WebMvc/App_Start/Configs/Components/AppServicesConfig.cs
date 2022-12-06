using EcommerceSandbox.AppServices.Interfaces.Services;
using EcommerceSandbox.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.AppServices"/>.
/// </summary>
public static class AppServicesConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.AppServices"/>. 
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddAppServicesConfig(this IServiceCollection services)
    {
        services.AddTransient<IProductAppService, ProductAppService>();
    }
}