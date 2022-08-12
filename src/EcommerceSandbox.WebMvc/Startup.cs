using EcommerceSandbox.WebMvc.Components;

namespace EcommerceSandbox.WebMvc;

/// <summary>
/// Application configurator.
/// </summary>
public class Startup
{
    private readonly IConfigurationRoot _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class with configuration provided.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfigurationRoot"/>.</param>
    public Startup(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }
    
    /// <summary>
    /// Configures application services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Components
        services.ConfigAppCore();
        services.ConfigAppServices();
        services.ConfigAppObjectStorage();
        services.ConfigDalEf();
        services.ConfigDalEfMssql(_configuration);
        services.ConfigHost();
        services.ConfigWebMvc();
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="env">The <see cref="IWebHostEnvironment"/>.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStatusCodePages();
        app.UseStaticFiles();
        app.UseMvcWithDefaultRoute();

        app.UseHttpLogging();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapControllers());

        app.ApplyMssqlMigration(_configuration);
    }
}