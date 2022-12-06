namespace EcommerceSandbox.DomainEntities.Entities;

/// <summary>
/// Product.
/// </summary>
public class Product
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
}