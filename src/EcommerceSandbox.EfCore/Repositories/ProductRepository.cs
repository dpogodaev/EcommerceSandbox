using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSandbox.EfCore.Entities;
using EcommerceSandbox.EfCore.Interfaces.DataContext;
using EcommerceSandbox.EfCore.Interfaces.Repositories;

namespace EcommerceSandbox.EfCore.Repositories;

/// <inheritdoc cref="ProductRepository"/>
public class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly IDataContext _dataContext;
    private readonly IEntityRepository<Product, long> _entityRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="dataContext">The database context.</param>
    public ProductRepository(
        IMapper mapper,
        IDataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
        _entityRepository = dataContext.GetRepository<Product, long>();
    }

    #region IProductRepository

    /// <inheritdoc cref="IProductRepository.GetByIdAsync"/>
    public async Task<Product> GetByIdAsync(long id)
    {
        return await _entityRepository.GetByIdAsync(id);
    }

    //TODO: to del
    public IEnumerable<Product> GetAll()
    {
        return _entityRepository.GetAll();
    }

    /// <inheritdoc cref="IProductRepository.CreateAsync"/>
    public async Task<Product> CreateAsync(Product entityToCreate)
    {
        var createdEntity = await _entityRepository.InsertAsync(entityToCreate);
        _dataContext.SaveChanges();

        return createdEntity;
    }

    /// <inheritdoc cref="IProductRepository.UpdateAsync"/>
    public async Task<Product> UpdateAsync(Product entityToUpdate)
    {
        var existingEntity = await _entityRepository.GetByIdAsync(entityToUpdate.Id);
        if (existingEntity == null) return null;

        _mapper.Map(entityToUpdate, existingEntity);
        await _entityRepository.UpdateAsync(existingEntity);
        _dataContext.SaveChanges();

        return existingEntity;
    }

    /// <inheritdoc cref="IProductRepository.BulkUpdateAsync"/>
    public async Task BulkUpdateAsync(IEnumerable<Product> entitiesToUpdate)
    {
        foreach (var entityToUpdate in entitiesToUpdate)
        {
            var existingEntity = await _entityRepository.GetByIdAsync(entityToUpdate.Id);
            if (existingEntity == null) continue;

            _mapper.Map(entityToUpdate, existingEntity);
            await _entityRepository.UpdateAsync(existingEntity);
        }

        _dataContext.SaveChanges();
    }

    /// <inheritdoc cref="IProductRepository.DeleteAsync"/>
    public async Task<Product> DeleteAsync(long id)
    {
        var entityToDelete = await _entityRepository.GetByIdAsync(id);
        if (entityToDelete == null) return null;

        await _entityRepository.DeleteAsync(entityToDelete);
        _dataContext.SaveChanges();

        return entityToDelete;
    }

    /// <inheritdoc cref="IProductRepository.SoftDeleteAsync"/>
    public async Task<Product> SoftDeleteAsync(long id)
    {
        var entityToDelete = await _entityRepository.GetByIdAsync(id);
        if (entityToDelete == null) return null;

        await _entityRepository.SoftDeleteAsync(entityToDelete);
        _dataContext.SaveChanges();

        return entityToDelete;
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
            CreateMap<Product, Product>().ReverseMap();
        }
    }
}