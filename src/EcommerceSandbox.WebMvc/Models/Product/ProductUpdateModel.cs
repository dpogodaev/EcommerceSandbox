namespace EcommerceSandbox.WebMvc.Models.Product;

public class ProductUpdateModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
}