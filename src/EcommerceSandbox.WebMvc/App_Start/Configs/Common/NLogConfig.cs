using System;
using EcommerceSandbox.WebMvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Web;

namespace EcommerceSandbox.WebMvc.Configs.Common;

/// <summary>
/// NLog configuration.
/// </summary>
public static class NLogConfig
{
    private const string NLogConfigParam = "NLog:ConfigFile";
    private const string NLogDefaultConfigFileName = "NLog.config";

    /// <summary>
    /// Adds a logger configuration for <c>NLog</c> platform.
    /// </summary>
    /// <param name="configuration">
    /// Represents a set of key/value application configuration properties.<br/>
    /// Omitted if the application builder is not yet available, for example, when launching the application.
    /// </param>
    public static void AddNLogConfig(IConfiguration configuration = null)
    {
        var nlogConfigFileName = GetNLogConfigFileName(configuration ?? GetAppConfiguration());

        LogManager.Configuration = new XmlLoggingConfiguration(nlogConfigFileName);
    }

    /// <summary>
    /// Adds the use of the <c>NLog</c> platform as a logger provider for Microsoft Extension Logging
    /// (<see cref="ILogger{TCategoryName}"/>).
    /// </summary>
    /// <param name="builder">A builder for web applications and services.</param>
    public static void UseNLogAsLoggerProvider(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }

    #region Private methods

    private static string GetNLogConfigFileName(IConfiguration configuration)
    {
        var nlogConfigFileName = configuration.GetProperty(NLogConfigParam);

        return string.IsNullOrEmpty(nlogConfigFileName)
            ? NLogDefaultConfigFileName
            : nlogConfigFileName;
    }

    private static IConfiguration GetAppConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    #endregion
}