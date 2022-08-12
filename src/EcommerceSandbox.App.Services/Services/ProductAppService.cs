using AutoMapper;
using EcommerceSandbox.App.Core.Interfaces.Adapters.ObjectStorage;
using EcommerceSandbox.App.Services.Dtos;
using EcommerceSandbox.App.Services.Models.Product;
using EcommerceSandbox.App.Domain;
using EcommerceSandbox.App.Services.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace EcommerceSandbox.App.Services.Services;

public class ProductAppService : IProductAppService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProductAppService> _logger;
    private readonly IProductStorageAdapter _storageAdapter;

    public ProductAppService(
        IMapper mapper,
        ILogger<ProductAppService> logger,
        IProductStorageAdapter storageAdapter)
    {
        _mapper = mapper;
        _logger = logger;
        _storageAdapter = storageAdapter;
    }

    #region IProductAppService

    public async Task<ProductDto> GetByIdAsync(long id)
    {
        var product = await _storageAdapter.GetByIdAsync(id);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _storageAdapter.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateAsync(ProductCreationModel creationModel)
    {
        var productToCreate = _mapper.Map<Product>(creationModel);

        var createdProduct = await _storageAdapter.CreateAsync(productToCreate);

        return _mapper.Map<ProductDto>(createdProduct);
    }

    public async Task<ProductDto> UpdateAsync(ProductUpdateModel model)
    {
        var existingProduct = await _storageAdapter.GetByIdAsync(model.Id);
        var productToUpdate = _mapper.Map<Product>(model);

        _mapper.Map(productToUpdate, existingProduct);

        var updatedProduct = await _storageAdapter.UpdateAsync(existingProduct);

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task<ProductDto> DeleteAsync(long id)
    {
        var deletedProduct = await _storageAdapter.DeleteAsync(id);

        return _mapper.Map<ProductDto>(deletedProduct);
    }

    #endregion

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, Product>();
            CreateMap<ProductCreationModel, Product>().ForMember(product => product.Id, opt => opt.Ignore());
            CreateMap<ProductUpdateModel, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}