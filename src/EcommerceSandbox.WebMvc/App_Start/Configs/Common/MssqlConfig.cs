using System;
using EcommerceSandbox.WebMvc.Extensions;
using EcommerceSandbox.WebMvc.Interfaces;
using EcommerceSandbox.WebMvc.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Common;

/// <summary>
/// MS SQL database connection configuration.<br/>
/// The <c>EntityFrameworkCore</c> is used as an ORM.
/// </summary>
public static class MssqlConfig
{
    private const string ConnectionString = "DefaultConnection";
    private const string SqlQueryLoggingConfigSection = "LoggingFeatures:SqlQueryLogging";

    /// <summary>
    /// Adds a configuration to connect to the MSSQL database.<br/>
    /// Also uses <see cref="SqlQueryLoggingSettings"/> to configure logging.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="logger">The logger.</param>
    /// <typeparam name="TContext">The database context.</typeparam>
    public static void AddSqlServerConfig<TContext>(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger) where TContext : DbContext
    {
        var connectionString = configuration.GetConnectionString(ConnectionString);

        if (string.IsNullOrEmpty(connectionString))
        {
            logger.Warn($"Connection string '{ConnectionString}' is not specified");
            return;
        }

        var loggingSettings = configuration.BindSection<SqlQueryLoggingSettings>(SqlQueryLoggingConfigSection);

        services.AddDbContext<TContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (loggingSettings?.EnableSensitiveDataLogging == true)
            {
                options.EnableSensitiveDataLogging();
            }
        });
    }

    /// <summary>
    /// Applies any pending migrations for the context to the MS SQL database.<br/>
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="app">Provides the mechanisms to configure an application's request pipeline.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <typeparam name="TContext">The database context.</typeparam>
    /// <exception cref="Exception">The database context is not configured.</exception>
    public static void ApplyMssqlMigration<TContext>(this IApplicationBuilder app, IConfiguration configuration)
        where TContext : DbContext
    {
        if (string.IsNullOrEmpty(configuration.GetConnectionString(ConnectionString)))
        {
            return;
        }

        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<TContext>();

        if (dbContext == null)
        {
            throw new Exception($"The {nameof(TContext)} is not configured");
        }

        dbContext.Database.Migrate();
    }
}