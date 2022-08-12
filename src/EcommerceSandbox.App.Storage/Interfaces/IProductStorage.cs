using EcommerceSandbox.App.Storage.PersistentDtos;

namespace EcommerceSandbox.App.Storage.Interfaces;

/// <summary>
/// Storage for product objects.
/// </summary>
public interface IProductStorage
{
    Task<ProductPDto> GetByIdAsync(long id);

    Task<IEnumerable<ProductPDto>> GetAllAsync();

    Task<ProductPDto> CreateAsync(ProductPDto product);

    Task<ProductPDto> UpdateAsync(ProductPDto product);

    Task<ProductPDto> DeleteAsync(long id);
}