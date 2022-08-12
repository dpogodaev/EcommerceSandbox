using EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;
using EcommerceSandbox.DAL.EF.Mssql.Context;
using EcommerceSandbox.DAL.EF.Mssql.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.WebMvc.Components;

public static class DalEfMssqlConfig
{
    private const string ConnectionName = "DefaultConnection";

    public static void ConfigDalEfMssql(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            return;
        }

        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    /// <summary>
    /// Applies migration for MS SQL database.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <exception cref="Exception">The <see cref="DataContext"/> is not configured.</exception>
    public static void ApplyMssqlMigration(this IApplicationBuilder app, IConfiguration configuration)
    {
        if (string.IsNullOrEmpty(configuration.GetConnectionString("DefaultConnection")))
        {
            return;
        }

        var services = app.ApplicationServices.CreateScope().ServiceProvider;

        var dbContext = services.GetService<DataContext>();

        if (dbContext == null)
        {
            throw new Exception($"The {nameof(DataContext)} is not configured");
        }

        dbContext.Database.Migrate();
    }
}