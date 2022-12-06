using EcommerceSandbox.AppStorages.Adapters.Repositories;
using EcommerceSandbox.DomainServices.Interfaces.Storages.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.DomainServices"/>.
/// </summary>
public static class DomainServicesConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.DomainServices"/>. 
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddDomainServicesConfig(this IServiceCollection services)
    {
        AddStorages(services);
    }

    #region Private methods

    private static void AddStorages(IServiceCollection services)
    {
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepositoryAdapter>();
        services.AddTransient<IUnitOfWork, UnitOfWorkAdapter>();
    }

    #endregion
}