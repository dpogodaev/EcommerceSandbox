using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppServices.Models.Product;

/// <summary>
/// Model for creating a <see cref="Product"/> entity.
/// </summary>
public class ProductCreationModel
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}