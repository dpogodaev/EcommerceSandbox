namespace EcommerceSandbox.WebMvc.Models.Product;

public class ProductCreationModel
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}