using Microsoft.Extensions.Logging;

namespace EcommerceSandbox.AppServices.Logging;

/// <summary>
/// Log events.
/// </summary>
internal static class AppLogEvents
{
    private const int BaseEventId = 1000;

    #region CRUD

    internal static readonly EventId Create = CreateEvenId(1, "Created");
    internal static readonly EventId Read = CreateEvenId(2, "Read");
    internal static readonly EventId Update = CreateEvenId(3, "Updated");
    internal static readonly EventId Delete = CreateEvenId(4, "Deleted");

    #endregion

    #region Not Found

    internal static readonly EventId ReadNotFound = CreateEvenId(101, "ReadNotFound");
    internal static readonly EventId UpdateNotFound = CreateEvenId(102, "UpdateNotFound");
    internal static readonly EventId DeleteNotFound = CreateEvenId(103, "DeleteNotFound");

    #endregion

    #region Private methods

    private static EventId CreateEvenId(int idOffset, string name) => new(BaseEventId + idOffset, name);

    #endregion
}