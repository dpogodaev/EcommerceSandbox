namespace EcommerceSandbox.WebMvc.Settings;

/// <summary>
/// SQL query logging settings.
/// </summary>
public class SqlQueryLoggingSettings
{
    /// <summary>
    /// Tells <c>EntityFrameworkCore</c> to include the parameter values in its logging messages. 
    /// </summary>
    public bool EnableSensitiveDataLogging { get; init; }
}