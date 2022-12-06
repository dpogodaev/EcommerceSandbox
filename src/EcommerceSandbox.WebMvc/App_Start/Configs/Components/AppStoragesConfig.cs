using EcommerceSandbox.AppStorages.Interfaces.Repositories;
using EcommerceSandbox.EfCore.Adapters;
using EcommerceSandbox.EfCore.DataContext;
using EcommerceSandbox.EfCoreMssql.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.AppStorages"/>.
/// </summary>
public static class AppStoragesConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.AppStorages"/>.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddAppStoragesConfig(this IServiceCollection services)
    {
        AddRepositories(services);
    }

    #region Private methods

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, DataContextFacade<MssqlDataContext>>(sp =>
            new DataContextFacade<MssqlDataContext>(sp.GetService<MssqlDataContext>()));

        services.AddTransient<IProductRepository, ProductRepositoryAdapter>();
    }

    #endregion
}