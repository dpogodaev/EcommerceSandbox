using Microsoft.Extensions.Logging;

namespace EcommerceSandbox.AppServices.Logging;

/// <summary>
/// Log events.
/// </summary>
internal static class AppLogEvents
{
    private const int BaseEventId = 1000;

    #region CRUD

    internal static readonly EventId Create = CreateEvenId(101, "Created");
    internal static readonly EventId Read = CreateEvenId(102, "Read");
    internal static readonly EventId ReadNotFound = CreateEvenId(112, "ReadNotFound");
    internal static readonly EventId Update = CreateEvenId(103, "Updated");
    internal static readonly EventId UpdateNotFound = CreateEvenId(113, "UpdateNotFound");
    internal static readonly EventId Delete = CreateEvenId(104, "Deleted");
    internal static readonly EventId DeleteNotFound = CreateEvenId(114, "DeleteNotFound");

    #endregion

    #region Private methods

    private static EventId CreateEvenId(int idOffset, string name) => new(BaseEventId + idOffset, name);

    #endregion
}