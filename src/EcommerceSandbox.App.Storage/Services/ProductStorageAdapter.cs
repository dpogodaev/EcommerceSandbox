using AutoMapper;
using EcommerceSandbox.App.Core.Interfaces.Adapters.ObjectStorage;
using EcommerceSandbox.App.Domain;
using EcommerceSandbox.App.Storage.Interfaces;
using EcommerceSandbox.App.Storage.PersistentDtos;

namespace EcommerceSandbox.App.Storage.Services;

public class ProductStorageAdapter : IProductStorageAdapter
{
    private readonly IMapper _mapper;
    private readonly IProductStorage _productStorage;

    public ProductStorageAdapter(
        IMapper mapper,
        IProductStorage productStorage)
    {
        _mapper = mapper;
        _productStorage = productStorage;
    }

    #region IProductStorageAdapter

    public async Task<Product> GetByIdAsync(long id)
    {
        var productDto = await _productStorage.GetByIdAsync(id);

        return _mapper.Map<Product>(productDto);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var productDto = await _productStorage.GetAllAsync();

        return _mapper.Map<IEnumerable<Product>>(productDto);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        var productDto = _mapper.Map<ProductPDto>(product);

        productDto.Id = 0;
        productDto.CreatedDate = DateTime.UtcNow;

        var createdProductDto = await _productStorage.CreateAsync(productDto);

        return _mapper.Map<Product>(createdProductDto);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var productDtoToUpdate = _mapper.Map<ProductPDto>(product);

        productDtoToUpdate.UpdatedDate = DateTime.UtcNow;

        var updatedProductDto = await _productStorage.UpdateAsync(productDtoToUpdate);

        return _mapper.Map<Product>(updatedProductDto);
    }

    public async Task<Product> DeleteAsync(long id)
    {
        var deletedProductDto = await _productStorage.DeleteAsync(id);

        return _mapper.Map<Product>(deletedProductDto);
    }

    #endregion

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductPDto, Product>().ReverseMap();
        }
    }
}