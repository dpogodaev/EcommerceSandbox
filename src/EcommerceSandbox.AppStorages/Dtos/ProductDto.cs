using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppStorages.Dtos;

/// <summary>
/// Persistent DTO for <see cref="Product"/> entity.
/// </summary>
public class ProductDto : PersistentDto<long>
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}