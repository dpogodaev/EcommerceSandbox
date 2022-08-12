using EcommerceSandbox.App.Domain;

namespace EcommerceSandbox.App.Storage.PersistentDtos;

/// <summary>
/// Persistent DTO for <see cref="Product"/> entity.
/// </summary>
public class ProductPDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Category.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Purchase price.
    /// </summary>
    public decimal PurchasePrice { get; set; }

    /// <summary>
    /// Retail price.
    /// </summary>
    public decimal RetailPrice { get; set; }

    /// <summary>
    /// Creation date.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Updated date.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Deleted date.
    /// </summary>
    public DateTime? DeletedDate { get; set; }
}