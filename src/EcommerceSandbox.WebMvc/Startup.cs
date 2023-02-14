using EcommerceSandbox.WebMvc.Configs.Common;
using EcommerceSandbox.WebMvc.Configs.Components;
using EcommerceSandbox.WebMvc.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace EcommerceSandbox.WebMvc;

/// <summary>
/// Application launch configurator.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Configures application services.
    /// </summary>
    /// <param name="builder">A builder for web applications and services.</param>
    /// <param name="logger">The logger.</param>
    public static void ConfigureServices(this WebApplicationBuilder builder, IStartupLogger logger)
    {
        // Components
        builder.Services.AddDomainServicesConfig();
        builder.Services.AddAppServicesConfig();
        builder.Services.AddAppStoragesConfig();
        builder.Services.AddEfCoreConfig();
        builder.Services.AddEfCoreMssqlConfig(builder.Configuration, logger);
        builder.Services.AddWebMvcConfig();

        // Common
        builder.Services.AddAutoMapperConfig();
        builder.Services.AddHttpLoggingConfig(builder.Configuration, logger);
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The web application used to configure the HTTP pipeline, and routes.</param>
    public static void ConfigureHttpRequestPipeline(this WebApplication app)
    {
        app.UseExceptionHandler(x => x.UseCustomExceptionHandler(app.Environment));
        app.UseStatusCodePages();
        app.UseStaticFiles();
        app.UseMvcWithDefaultRoute();

        // app.UseHttpLogging();
        // app.UseHttpsRedirection();
        // app.UseRouting();
        // app.UseAuthentication();
        // app.UseAuthorization();
        // app.UseEndpoints(x => x.MapControllers());
    }

    /// <summary>
    /// Applies automatic migration.
    /// </summary>
    /// <param name="app">The web application used to configure the HTTP pipeline, and routes.</param>
    public static void ApplyAutoMigration(this WebApplication app)
    {
        app.ApplyMssqlMigration(app.Configuration);
    }
}