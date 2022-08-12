using NLog;
using NLog.Web;

using EcommerceSandbox.WebMvc;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("Init main");

    var builder = WebApplication.CreateBuilder(args);

    var startup = new Startup(builder.Configuration);

    startup.ConfigureServices(builder.Services);

    //builder.AddNLogConfig();

    var app = builder.Build();

    startup.Configure(app, app.Environment);

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}