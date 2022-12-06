using EcommerceSandbox.EfCore.DataContext;
using EcommerceSandbox.EfCore.Interfaces.DataContext;
using EcommerceSandbox.EfCore.Interfaces.Repositories;
using EcommerceSandbox.EfCore.Repositories;
using EcommerceSandbox.EfCoreMssql.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.EfCore"/>.
/// </summary>
public static class EfCoreConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.EfCore"/>. 
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    public static void AddEfCoreConfig(this IServiceCollection services)
    {
        AddDataContext(services);
        AddRepositories(services);
    }

    #region Private methods

    private static void AddDataContext(IServiceCollection services)
    {
        services.AddScoped<IDataContext, DataContextFacade<MssqlDataContext>>(sp =>
            new DataContextFacade<MssqlDataContext>(sp.GetService<MssqlDataContext>()));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
    }

    #endregion
}