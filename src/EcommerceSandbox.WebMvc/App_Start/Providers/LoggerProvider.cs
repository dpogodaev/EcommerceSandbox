using EcommerceSandbox.WebMvc.Configs.Common;
using Microsoft.AspNetCore.Builder;

namespace EcommerceSandbox.WebMvc.Providers;

/// <summary>
/// Logger provider for Microsoft Extension Logging.
/// </summary>
public static class LoggerProvider
{
    private static bool _isConfigured;

    /// <summary>
    /// Indicates if the logger provider is configured.
    /// </summary>
    public static bool IsConfigured() => _isConfigured;

    /// <summary>
    /// Configures the logger provider.
    /// </summary>
    public static void Configure()
    {
        NLogConfig.AddNLogConfig();

        _isConfigured = true;
    }

    /// <summary>
    /// Adds the use of the logger provider for working
    /// with <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/> objects.
    /// </summary>
    /// <param name="builder">A builder for web applications and services.</param>
    public static void UseLoggerProvider(this WebApplicationBuilder builder)
    {
        builder.UseNLogAsLoggerProvider();
    }
}