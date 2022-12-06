namespace EcommerceSandbox.WebMvc.Dtos;

/// <summary>
/// DTO for product entity.
/// </summary>
public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}