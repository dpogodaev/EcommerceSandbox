using EcommerceSandbox.App.Storage.Interfaces;
using EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;
using EcommerceSandbox.DAL.EF.Mssql.UnitOfWork;
using EcommerceSandbox.DAL.EF.Storages;

namespace EcommerceSandbox.WebMvc.Components;

public static class DalEfConfig
{
    public static void ConfigDalEf(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductStorage, ProductStorage>();
    }
}