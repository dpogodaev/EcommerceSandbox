using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly IProductRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductAppService"/> class.
    /// </summary>
    /// <param name="mapper">The object mapper.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="repository">The repository for <see cref="Product"/>s.</param>
    public ProductAppService(
        IMapper mapper,
        ILogger<ProductAppService> logger,
        IProductRepository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    #region IProductAppService

    /// <inheritdoc cref="IProductAppService.GetByIdAsync"/>
    public async Task<ProductDto> GetByIdAsync(long id)
    {
        try
        {
            var foundEntity = await _repository.GetByIdAsync(id);
            if (foundEntity == null)
            {
                LogNotFound(id);
                return null;
            }

            var foundDto = _mapper.Map<ProductDto>(foundEntity);

            LogGet(foundDto);
            return foundDto;
        }
        catch (Exception e)
        {
            LogFailedGet(e, id);
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.GetAllAsync"/>
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        try
        {
            var foundEntities = (await _repository.GetAllAsync()).ToList();
            var foundDtos = _mapper.Map<IEnumerable<ProductDto>>(foundEntities);

            LogGetAll(foundEntities.Count);
            return foundDtos;
        }
        catch (Exception e)
        {
            LogFailedGetAll(e);
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.CreateAsync"/>
    public async Task<ProductDto> CreateAsync(ProductCreationModel modelToCreate)
    {
        try
        {
            var entityToCrete = _mapper.Map<Product>(modelToCreate);
            var createdEntity = await _repository.CreateAsync(entityToCrete);
            var createdDto = _mapper.Map<ProductDto>(createdEntity);

            LogCreated(createdDto);
            return createdDto;
        }
        catch (Exception e)
        {
            LogFailedCreate(e, modelToCreate);
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.UpdateAsync"/>
    public async Task<ProductDto> UpdateAsync(ProductUpdateModel modelToUpdate)
    {
        try
        {
            var entityToUpdate = await _repository.GetByIdAsync(modelToUpdate.Id);
            if (entityToUpdate == null)
            {
                LogUpdateNotFound(modelToUpdate);
                return null;
            }

            _mapper.Map(modelToUpdate, entityToUpdate);

            var updatedEntity = await _repository.UpdateAsync(entityToUpdate);
            var updatedDto = _mapper.Map<ProductDto>(updatedEntity);

            LogUpdated(updatedDto);
            return updatedDto;
        }
        catch (Exception e)
        {
            LogFailedUpdate(e, modelToUpdate);
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
                var entityToUpdate = await _repository.GetByIdAsync(modelToUpdate.Id);
                if (entityToUpdate == null)
                {
                    LogUpdateNotFound(modelToUpdate);
                    continue;
                }

                LogUpdated(modelToUpdate);

                _mapper.Map(modelToUpdate, entityToUpdate);
                entitiesToUpdate.Add(entityToUpdate);
            }

            await _repository.BulkUpdateAsync(entitiesToUpdate);
        }
        catch (Exception e)
        {
            LogFailedUpdates(e);
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.DeleteAsync"/>
    public async Task<ProductDto> DeleteAsync(long id)
    {
        try
        {
            var deletedEntity = await _repository.DeleteAsync(id);
            if (deletedEntity == null)
            {
                LogDeleteNotFound(id);
                return null;
            }

            var deletedDto = _mapper.Map<ProductDto>(deletedEntity);

            LogDeleted(deletedDto);
            return deletedDto;
        }
        catch (Exception e)
        {
            LogFailedDelete(e, id);
            throw;
        }
    }

    #endregion

    #region Private methods

    private void LogGet(ProductDto foundDto)
    {
        _logger.LogDebug(AppLogEvents.Read, "{Title}: id={ProductId}, {Details}",
            "The product was found", foundDto.Id, $"dto={Serialize(foundDto)}");
    }

    private void LogGetAll(int count)
    {
        _logger.LogDebug(AppLogEvents.Read, "{Title}: count={ProductCount}",
            "All products have been returned", count);
    }

    private void LogCreated(ProductDto createdDto)
    {
        _logger.LogDebug(AppLogEvents.Create, "{Title}: id={ProductId}, {Details}",
            "The product was created", createdDto.Id, $"dto={Serialize(createdDto)}");
    }

    private void LogUpdated(ProductDto updatedDto)
    {
        _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
            "The product has been updated", updatedDto.Id, $"dto={Serialize(updatedDto)}");
    }

    private void LogUpdated(ProductUpdateModel modelToUpdate)
    {
        _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
            "The product has been updated", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogDeleted(ProductDto deletedDto)
    {
        _logger.LogDebug(AppLogEvents.Delete, "{Title}: id={ProductId}, {Details}",
            "The product has been deleted", deletedDto.Id, $"dto={Serialize(deletedDto)}");
    }

    private void LogNotFound(long id)
    {
        _logger.LogDebug(AppLogEvents.ReadNotFound, "{Title}: id={ProductId}",
            "The product was not found", id);
    }

    private void LogUpdateNotFound(ProductUpdateModel modelToUpdate)
    {
        _logger.LogDebug(AppLogEvents.UpdateNotFound, "{Title}: id={ProductId}, {Details}",
            "The product to update was not found", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogDeleteNotFound(long id)
    {
        _logger.LogDebug(AppLogEvents.DeleteNotFound, "{Title}: id={ProductId}",
            "The product to delete was not found", id);
    }

    private void LogFailedGet(Exception e, long id)
    {
        _logger.LogError(AppLogEvents.Read, e, "{Title}: id={ProductId}",
            "Failed to found the product", id);
    }

    private void LogFailedGetAll(Exception e)
    {
        _logger.LogError(AppLogEvents.Read, e, "{Title}", "Failed to get all products");
    }

    private void LogFailedCreate(Exception e, ProductCreationModel modelToCreate)
    {
        _logger.LogError(AppLogEvents.Create, e, "{Title}: {Details}",
            "Failed to create a product", $"model={Serialize(modelToCreate)}");
    }

    private void LogFailedUpdate(Exception e, ProductUpdateModel modelToUpdate)
    {
        _logger.LogError(AppLogEvents.Update, e, "{Title}: id={ProductId}, {Details}",
            "Failed to update the product", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogFailedUpdates(Exception e)
    {
        _logger.LogError(AppLogEvents.Update, e, "{Title}", "Failed to update products");
    }

    private void LogFailedDelete(Exception e, long id)
    {
        _logger.LogError(e, "{Title}: id={ProductId}", "Failed to delete the product", id);
    }

    private static string Serialize<T>(T source) where T : class
    {
        return StringHelper.Serialize(source);
    }

    #endregion

    /// <summary>
    /// Mapping rules for <see cref="EcommerceSandbox.AppServices"/>
    /// and <see cref="EcommerceSandbox.DomainEntities.Entities"/>.
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