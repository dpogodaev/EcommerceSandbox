using System;
using EcommerceSandbox.WebMvc;
using EcommerceSandbox.WebMvc.Providers;
using Microsoft.AspNetCore.Builder;

LoggerProvider.Configure();
var startupLogger = StartupLoggerProvider.GetLogger();

try
{
    startupLogger.Debug("Program: application configure started");

    var builder = WebApplication.CreateBuilder(args);
    builder.UseLoggerProvider();
    builder.ConfigureServices(startupLogger);

    startupLogger.Debug("Program: application build started");

    var app = builder.Build();
    app.ConfigureHttpRequestPipeline();
    app.ApplyAutoMigration();

    startupLogger.Debug("Program: application has been successfully launched");

    app.Run();
}
catch (Exception e)
{
    startupLogger.Error(e, "Program: failed to launch the application");
    throw;
}
finally
{
    startupLogger.Dispose();
}