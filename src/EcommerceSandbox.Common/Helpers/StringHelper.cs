using Newtonsoft.Json;

namespace EcommerceSandbox.Common.Helpers;

/// <summary>
/// String objects helper.
/// </summary>
public class StringHelper
{
    /// <summary>
    /// Serializes an object of the specified type into a string.
    /// </summary>
    /// <param name="source">Source object.</param>
    /// <typeparam name="T">Object type.</typeparam>
    /// <returns>
    /// Serialized string value if the <paramref name="source"/> is not <c>null</c>;
    /// <c><see cref="string.Empty"/></c> otherwise.
    /// </returns>
    public static string Serialize<T>(T source) where T : class
    {
        return source != null 
            ? JsonConvert.SerializeObject(source)
            : string.Empty;
    }

    /// <summary>
    /// Deserializes a string into an object of the specified type.
    /// </summary>
    /// <param name="source">String in JSON format.</param>
    /// <typeparam name="T">Object type.</typeparam>
    /// <returns>Deserialized object of the specified type.</returns>
    /// <returns>
    /// Deserialized object if the <paramref name="source"/> is not <c>null</c> or <c><see cref="string.Empty"/></c>;
    /// <c>null</c> otherwise.
    /// </returns>
    public static T Deserialize<T>(string source) where T : class
    {
        return !string.IsNullOrEmpty(source)
            ? JsonConvert.DeserializeObject<T>(source)
            : null;
    }

    /// <summary>
    /// Serializes an object of the specified type to string for structured logging with arguments.
    /// </summary>
    /// <param name="source">Source object.</param>
    /// <typeparam name="T">Object type.</typeparam>
    /// <returns>
    /// Serialized string value if the <paramref name="source"/> is not <c>null</c>;
    /// <c><see cref="string.Empty"/></c> otherwise.
    /// </returns>
    /// <remarks>Curly braces escaping is used.</remarks>
    public static string SerializeForStructLog<T>(T source) where T : class
    {
        return source != null
            ? EscapeCurlyBraces(Serialize(source))
            : string.Empty;
    }

    #region Private methods

    private static string EscapeCurlyBraces(string source)
    {
        return source
            .Replace("{", "{{")
            .Replace("}", "}}");
    }

    #endregion
}