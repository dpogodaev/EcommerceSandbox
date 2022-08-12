using EcommerceSandbox.App.Domain;

namespace EcommerceSandbox.App.Core.Interfaces.Adapters.ObjectStorage;

public interface IProductStorageAdapter
{
    Task<Product> GetByIdAsync(long id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> CreateAsync(Product product);

    Task<Product> UpdateAsync(Product product);

    Task<Product> DeleteAsync(long id);
}