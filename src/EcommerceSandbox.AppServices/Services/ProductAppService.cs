using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceSandbox.AppServices.Dtos;
using EcommerceSandbox.AppServices.Interfaces.Services;
using EcommerceSandbox.AppServices.Logging;
using EcommerceSandbox.AppServices.Models.Product;
using EcommerceSandbox.Common.Helpers;
using EcommerceSandbox.DomainEntities.Entities;
using EcommerceSandbox.DomainServices.Interfaces.Storages.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceSandbox.AppServices.Services;

/// <inheritdoc cref="IProductAppService"/>
public class ProductAppService : IProductAppService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProductAppService> _logger;
    private readonly IProductRepository _storage;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductAppService"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="storage">The storage for <see cref="Product"/>s.</param>
    public ProductAppService(
        IMapper mapper,
        ILogger<ProductAppService> logger,
        IProductRepository storage)
    {
        _mapper = mapper;
        _logger = logger;
        _storage = storage;
    }

    #region IProductAppService

    /// <inheritdoc cref="IProductAppService.GetByIdAsync"/>
    public async Task<ProductDto> GetByIdAsync(long id)
    {
        try
        {
            var foundEntity = await _storage.GetByIdAsync(id);
            if (foundEntity == null)
            {
                _logger.LogDebug(AppLogEvents.ReadNotFound, "{Title}: id={ProductId}",
                    "The product was not found", id);

                return null;
            }

            var foundDto = _mapper.Map<ProductDto>(foundEntity);

            _logger.LogDebug(AppLogEvents.Read, "{Title}: id={ProductId}, {Details}",
                "The product was found", id, $"dto={Serialize(foundDto)}");

            return foundDto;
        }
        catch (Exception e)
        {
            _logger.LogError(AppLogEvents.Read, e, "{Title}: id={ProductId}",
                "Failed to found the product", id);
            throw;
        }
    }

    //TODO: to del, add filter
    public IEnumerable<ProductDto> GetAll()
    {
        var foundEntities = _storage.GetAll();
        var foundDtos = _mapper.Map<IEnumerable<ProductDto>>(foundEntities);

        return foundDtos;
    }

    /// <inheritdoc cref="IProductAppService.CreateAsync"/>
    public async Task<ProductDto> CreateAsync(ProductCreationModel modelToCreate)
    {
        try
        {
            var entityToCrete = _mapper.Map<Product>(modelToCreate);
            var createdEntity = await _storage.CreateAsync(entityToCrete);
            var createdDto = _mapper.Map<ProductDto>(createdEntity);

            _logger.LogDebug(AppLogEvents.Create, "{Title}: id={ProductId}, {Details}",
                "The product was created", createdDto.Id, $"dto={Serialize(createdDto)}");

            return createdDto;
        }
        catch (Exception e)
        {
            _logger.LogError(AppLogEvents.Create, e, "{Title}: {Details}",
                "Failed to create a product", $"model={Serialize(modelToCreate)}");
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.UpdateAsync"/>
    public async Task<ProductDto> UpdateAsync(ProductUpdateModel modelToUpdate)
    {
        try
        {
            var entityToUpdate = await _storage.GetByIdAsync(modelToUpdate.Id);
            if (entityToUpdate == null)
            {
                _logger.LogDebug(AppLogEvents.UpdateNotFound, "{Title}: id={ProductId}, {Details}",
                    "The product was not found", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");

                return null;
            }

            _mapper.Map(modelToUpdate, entityToUpdate);

            var updatedEntity = await _storage.UpdateAsync(entityToUpdate);
            var updatedDto = _mapper.Map<ProductDto>(updatedEntity);

            _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
                "The product to update has been updated", modelToUpdate.Id, $"dto={Serialize(updatedDto)}");

            return updatedDto;
        }
        catch (Exception e)
        {
            _logger.LogError(AppLogEvents.Update, e, "{Title}: id={ProductId}, {Details}",
                "Failed to update the product", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.BulkUpdateAsync"/>
    public async Task BulkUpdateAsync(IEnumerable<ProductUpdateModel> modelsToUpdate)
    {
        try
        {
            var entitiesToUpdate = new List<Product>();

            foreach (var modelToUpdate in modelsToUpdate)
            {
                var entityToUpdate = await _storage.GetByIdAsync(modelToUpdate.Id);
                if (entityToUpdate == null)
                {
                    _logger.LogDebug(AppLogEvents.UpdateNotFound, "{Title}: id={ProductId}, {Details}",
                        "The product to update was not found", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");

                    continue;
                }

                _mapper.Map(modelToUpdate, entityToUpdate);

                entitiesToUpdate.Add(entityToUpdate);
            }

            await _storage.BulkUpdateAsync(entitiesToUpdate);

            entitiesToUpdate.ForEach(x =>
                _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
                    "The product has been updated", x.Id, $"dto={Serialize(x)}"));
        }
        catch (Exception e)
        {
            _logger.LogError(AppLogEvents.Update, e, "{Title}", "Failed to update products");
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.DeleteAsync"/>
    public async Task<ProductDto> DeleteAsync(long id)
    {
        try
        {
            var deletedEntity = await _storage.DeleteAsync(id);
            if (deletedEntity == null)
            {
                _logger.LogDebug(AppLogEvents.DeleteNotFound, "{Title}: id={ProductId}",
                    "The product to delete was not found", id);

                return null;
            }

            var deletedDto = _mapper.Map<ProductDto>(deletedEntity);

            _logger.LogDebug(AppLogEvents.Delete, "{Title}: id={ProductId}, {Details}",
                "The product has been deleted", id, $"dto={Serialize(deletedDto)}");

            return deletedDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Title}: id={ProductId}", "Failed to delete the product", id);
            throw;
        }
    }

    #endregion

    private static string Serialize<T>(T source) where T : class
    {
        return StringHelper.Serialize(source);
    }

    /// <summary>
    /// Mapping rules for <see cref="EcommerceSandbox.AppServices"/> and <see cref="EcommerceSandbox.DomainEntities.Entities"/>.
    /// </summary>
    internal class Mapping : Profile
    {
        /// <summary>
        /// Sets mapping rules.
        /// </summary>
        public Mapping()
        {
            CreateMap<Product, Product>();
            CreateMap<ProductCreationModel, Product>();
            CreateMap<ProductUpdateModel, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}