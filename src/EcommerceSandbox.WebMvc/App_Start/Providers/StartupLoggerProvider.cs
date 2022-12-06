using System;
using EcommerceSandbox.WebMvc.Interfaces;
using NLog;

namespace EcommerceSandbox.WebMvc.Providers;

/// <summary>
/// Logger provider for launching the application.
/// </summary>
public class StartupLoggerProvider
{
    /// <summary>
    /// Returns instance of the <see cref="IStartupLogger"/> class.
    /// </summary>
    public static IStartupLogger GetLogger()
    {
        if (LoggerProvider.IsConfigured())
        {
            return new StartupLogger();
        }

        throw new Exception("The logger provider is not configured");
    }

    private class StartupLogger : IStartupLogger
    {
        private readonly Logger _logger;

        public StartupLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        #region IStartupLogger

        /// <inheritdoc cref="IStartupLogger.Debug"/>
        public void Debug(string msg) => _logger.Debug(msg);

        /// <inheritdoc cref="IStartupLogger.Warn"/>
        public void Warn(string msg) => _logger.Warn(msg);

        /// <inheritdoc cref="IStartupLogger.Error"/>
        public void Error(Exception e, string msg) => _logger.Error(e, msg);

        #endregion

        #region IDisposable

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            LogManager.Shutdown();
        }

        #endregion
    }
}