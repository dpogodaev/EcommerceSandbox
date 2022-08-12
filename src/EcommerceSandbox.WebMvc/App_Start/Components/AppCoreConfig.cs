using EcommerceSandbox.App.Core.Interfaces.Adapters.ObjectStorage;
using AppObjectStorage = EcommerceSandbox.App.Storage.Services; 

namespace EcommerceSandbox.WebMvc.Components;

public static class AppCoreConfig
{
    public static void ConfigAppCore(this IServiceCollection services)
    {
        services.AddTransient<IProductStorageAdapter, AppObjectStorage.ProductStorageAdapter>();
    }
}