namespace EcommerceSandbox.WebMvc.Models.Product;

/// <summary>
/// Model for creating a product.
/// </summary>
public class ProductCreationModel
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}