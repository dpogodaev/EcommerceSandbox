using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppServices.Dtos;

/// <summary>
/// DTO for <see cref="Product"/> entity.
/// </summary>
public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}