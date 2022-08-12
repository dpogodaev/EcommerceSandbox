using EcommerceSandbox.App.Services.Dtos;
using EcommerceSandbox.App.Services.Models.Product;

namespace EcommerceSandbox.App.Services.Interfaces.Services;

/// <summary>
/// Application service for performing operations with products.
/// </summary>
public interface IProductAppService
{
    Task<ProductDto> GetByIdAsync(long id);

    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto> CreateAsync(ProductCreationModel creationModel);

    Task<ProductDto> UpdateAsync(ProductUpdateModel product);

    Task<ProductDto> DeleteAsync(long id);
}