using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSandbox.AppStorages.Dtos;
using EcommerceSandbox.EfCore.Entities;
using EcommerceSandbox.EfCore.Interfaces.Repositories;

namespace EcommerceSandbox.EfCore.Adapters;

/// <summary>
/// Adapter for <see cref="AppStorages.Interfaces.Repositories.IProductRepository"/>.
/// </summary>
public class ProductRepositoryAdapter : AppStorages.Interfaces.Repositories.IProductRepository
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepositoryAdapter"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="repository">Repository for <see cref="Product"/>s.</param>
    public ProductRepositoryAdapter(
        IMapper mapper,
        IProductRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    #region IProductRepository

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.GetByIdAsync"/>
    public async Task<ProductDto> GetByIdAsync(long id)
    {
        return _mapper.Map<ProductDto>(
            await _repository.GetByIdAsync(id));
    }

    //TODO: to del
    public IEnumerable<ProductDto> GetAll()
    {
        return _mapper.Map<IEnumerable<ProductDto>>(
            _repository.GetAll());
    }

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.CreateAsync"/>
    public async Task<ProductDto> CreateAsync(ProductDto dtoToCreate)
    {
        return _mapper.Map<ProductDto>(
            await _repository.CreateAsync(
                _mapper.Map<Product>(dtoToCreate)));
    }

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.UpdateAsync"/>
    public async Task<ProductDto> UpdateAsync(ProductDto dtoToUpdate)
    {
        return _mapper.Map<ProductDto>(
            await _repository.UpdateAsync(
                _mapper.Map<Product>(dtoToUpdate)));
    }

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.BulkUpdateAsync"/>
    public async Task BulkUpdateAsync(IEnumerable<ProductDto> dtosToUpdate)
    {
        await _repository.BulkUpdateAsync(
            _mapper.Map<IEnumerable<Product>>(dtosToUpdate));
    }

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.DeleteAsync"/>
    public async Task<ProductDto> DeleteAsync(long id)
    {
        return _mapper.Map<ProductDto>(
            await _repository.DeleteAsync(id));
    }

    /// <inheritdoc cref="AppStorages.Interfaces.Repositories.IProductRepository.SoftDeleteAsync"/>
    public async Task<ProductDto> SoftDeleteAsync(long id)
    {
        return _mapper.Map<ProductDto>(
            await _repository.SoftDeleteAsync(id));
    }

    #endregion

    /// <summary>
    /// Mapping rules for <see cref="EcommerceSandbox.EfCore.Entities"/> and <see cref="EcommerceSandbox.AppStorage.Dtos"/>.
    /// </summary>
    internal class Mapping : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public Mapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}