namespace EcommerceSandbox.WebMvc.Components;

public static class HostConfig
{
    public static void ConfigHost(this IServiceCollection services)
    {
        ConfigAutomapper(services);
    }

    /// <summary>
    /// Adds a configuration for automapper.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    private static void ConfigAutomapper(IServiceCollection services)
    {
        const string solution = $"{nameof(EcommerceSandbox)}";
        const string appLayer = $"{solution}.{nameof(App)}";
        const string dalLayer = $"{solution}.{nameof(DAL)}";

        services.AddAutoMapper(x => x.AddMaps(
            $"{appLayer}.{nameof(App.Core)}",
            $"{appLayer}.{nameof(App.Services)}",
            $"{appLayer}.{nameof(App.Storage)}",
            $"{dalLayer}.{nameof(DAL.EF)}",
            $"{solution}.{nameof(WebMvc)}"));
    }
}