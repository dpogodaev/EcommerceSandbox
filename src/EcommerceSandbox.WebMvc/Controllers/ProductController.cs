using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.WebMvc.Dtos;
using EcommerceSandbox.WebMvc.Interfaces;
using EcommerceSandbox.WebMvc.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSandbox.WebMvc.Controllers;

/// <summary>
/// Controller for working with products.
/// </summary>
public class ProductController : Controller
{
    private readonly IProductService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="service">Used for performing operations with products.</param>
    public ProductController(IProductService service)
    {
        _service = service;
    }

    /// <summary>
    /// Returns a list of products.
    /// </summary>
    /// <returns><see cref="ProductDto"/>s of found products.</returns>
    public async Task<IActionResult> Index()
    {
        return View(_service.GetAll());
    }

    /// <summary>
    /// Adds a new product.
    /// </summary>
    /// <param name="model">The <see cref="ProductCreationModel"/> to create a product.</param>
    /// <returns>The <see cref="ProductDto"/> of the created product.</returns>
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductCreationModel model)
    {
        await _service.AddProduct(model);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="model">The <see cref="ProductUpdateModel"/> to update the product.</param>
    /// <returns>The <see cref="ProductDto"/> of the updated product.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductUpdateModel model)
    {
        await _service.UpdateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Updates existing products.
    /// </summary>
    /// <param name="models"><see cref="ProductUpdateModel"/>s to update products.</param>
    /// <returns>The <see cref="Task"/> that will be completed when all products are updated.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateAll(IEnumerable<ProductUpdateModel> models)
    {
        await _service.BulkUpdateAsync(models);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpdateProduct(long key)
    {
        return View(await _service.GetByIdAsync(key));
    }

    public async Task<IActionResult> UpdateAll()
    {
        ViewBag.UpdateAll = true;

        return View(nameof(Index), _service.GetAll());
    }
}