using EcommerceSandbox.App.Services.Interfaces.Services;
using EcommerceSandbox.App.Services.Services;

namespace EcommerceSandbox.WebMvc.Components;

public static class AppServicesConfig
{
    public static void ConfigAppServices(this IServiceCollection services)
    {
        services.AddTransient<IProductAppService, ProductAppService>();
    }
}