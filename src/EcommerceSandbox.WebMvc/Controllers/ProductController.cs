using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.WebMvc.Interfaces;
using EcommerceSandbox.WebMvc.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSandbox.WebMvc.Controllers;

/// <summary>
/// Used to perform CRUD-operations with products.
/// </summary>
public class ProductController : Controller
{
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productService">Used for performing operations with products.</param>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Returns a list of all products.
    /// </summary>
    /// <returns>A list of all found products.</returns>
    public async Task<IActionResult> Index()
    {
        var allProducts = await _productService.GetAllAsync();

        return View(allProducts);
    }

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="product">The model to create a product.</param>
    /// <returns>A list of all products.</returns>
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductCreationModel product)
    {
        await _productService.AddAsync(product);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="product">The model to update the product.</param>
    /// <returns>A list of all products.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductUpdateModel product)
    {
        await _productService.UpdateAsync(product);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Updates existing products.
    /// </summary>
    /// <param name="products">Models to update products.</param>
    /// <returns>A list of all products.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProducts(IEnumerable<ProductUpdateModel> products)
    {
        await _productService.BulkUpdateAsync(products);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateProduct(long id)
    {
        var foundProduct = await _productService.GetByIdAsync(id); 

        return View(foundProduct);
    }

    public async Task<IActionResult> UpdateAllProducts()
    {
        ViewBag.UpdateAll = true;

        var allProducts = await _productService.GetAllAsync();

        return View(nameof(Index), allProducts);
    }
}