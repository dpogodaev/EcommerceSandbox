using System;
using EcommerceSandbox.EfCoreMssql.DataContext;
using EcommerceSandbox.WebMvc.Configs.Common;
using EcommerceSandbox.WebMvc.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceSandbox.WebMvc.Configs.Components;

/// <summary>
/// Configuration of component <see cref="EcommerceSandbox.EfCoreMssql"/>.
/// </summary>
public static class EfCoreMssqlConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="EcommerceSandbox.EfCoreMssql"/>. 
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <param name="logger">The logger.</param>
    public static void AddEfCoreMssqlConfig(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger)
    {
        services.AddSqlServerConfig<MssqlDataContext>(configuration, logger);
    }

    /// <summary>
    /// Applies any pending migrations for the context to the MS SQL database.<br/>
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="app">Provides the mechanisms to configure an application's request pipeline.</param>
    /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
    /// <exception cref="Exception">Thrown when the database context is not configured.</exception>
    public static void ApplyMssqlMigration(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.ApplyMssqlMigration<MssqlDataContext>(configuration);
    }
}