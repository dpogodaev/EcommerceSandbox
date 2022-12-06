using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppServices.Models.Product;

/// <summary>
/// Model for updating the <see cref="Product"/> entity.
/// </summary>
public class ProductUpdateModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}