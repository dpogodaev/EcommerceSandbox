namespace EcommerceSandbox.AppStorages.Dtos;

/// <summary>
/// Persistent DTO.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
public abstract class PersistentDto<TPrimaryKey>
{
    /// <summary>
    /// Unique identifier (primary key).
    /// </summary>
    public TPrimaryKey Id { get; set; }
}