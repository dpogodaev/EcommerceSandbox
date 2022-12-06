namespace EcommerceSandbox.EfCore.Entities;

/// <summary>
/// Persistent entity.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
public abstract class PersistentEntity<TPrimaryKey>
{
    /// <summary>
    /// Unique identifier (primary key).
    /// </summary>
    public TPrimaryKey Id { get; set; }
}