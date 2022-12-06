using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSandbox.AppStorages.Dtos;
using EcommerceSandbox.DomainEntities.Entities;
using DomainRepositories = EcommerceSandbox.DomainServices.Interfaces.Storages.Repositories;
using IProductRepository = EcommerceSandbox.AppStorages.Interfaces.Repositories.IProductRepository;

namespace EcommerceSandbox.AppStorages.Adapters.Repositories;

/// <summary>
/// Adapter for <see cref="DomainRepositories.IProductRepository"/>.
/// </summary>
public class ProductRepositoryAdapter : DomainRepositories.IProductRepository
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepositoryAdapter"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="repository">The storage for <see cref="Product"/>s.</param>
    public ProductRepositoryAdapter(
        IMapper mapper,
        IProductRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    #region IProductRepository

    /// <inheritdoc cref="DomainRepositories.IProductRepository.GetByIdAsync"/>
    public async Task<Product> GetByIdAsync(long id)
    {
        return _mapper.Map<Product>(
            await _repository.GetByIdAsync(id));
    }

    /// <inheritdoc cref="DomainRepositories.IProductRepository.GetAll"/>
    public IEnumerable<Product> GetAll()
    {
        return _mapper.Map<IEnumerable<Product>>(
            _repository.GetAll());
    }

    /// <inheritdoc cref="DomainRepositories.IProductRepository.CreateAsync"/>
    public async Task<Product> CreateAsync(Product entityToCreate)
    {
        return _mapper.Map<Product>(
            await _repository.CreateAsync(
                _mapper.Map<ProductDto>(entityToCreate)));
    }

    /// <inheritdoc cref="DomainRepositories.IProductRepository.UpdateAsync"/>
    public async Task<Product> UpdateAsync(Product entityToUpdate)
    {
        return _mapper.Map<Product>(
            await _repository.UpdateAsync(
                _mapper.Map<ProductDto>(entityToUpdate)));
    }

    /// <inheritdoc cref="DomainRepositories.IProductRepository.BulkUpdateAsync"/>
    public async Task BulkUpdateAsync(IEnumerable<Product> entitiesToUpdate)
    {
        await _repository.BulkUpdateAsync(
            _mapper.Map<IEnumerable<ProductDto>>(entitiesToUpdate));
    }

    /// <inheritdoc cref="DomainRepositories.IProductRepository.DeleteAsync"/>
    public async Task<Product> DeleteAsync(long id)
    {
        return _mapper.Map<Product>(
            await _repository.DeleteAsync(id));
    }

    #endregion

    /// <summary>
    /// Mapping rules for <see cref="EcommerceSandbox.AppStorage.Dtos"/> and <see cref="EcommerceSandbox.DomainEntities.Entities"/>.
    /// </summary>
    internal class Mapping : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public Mapping()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}