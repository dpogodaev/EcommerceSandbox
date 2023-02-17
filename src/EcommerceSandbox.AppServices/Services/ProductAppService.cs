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
                LogGettingNotFound(id);
                return null;
            }

            var foundDto = _mapper.Map<ProductDto>(foundEntity);

            LogGetting(foundDto);
            return foundDto;
        }
        catch (Exception e)
        {
            LogGettingError(e, id);
            throw;
        }
    }

    /// <inheritdoc cref="IProductAppService.GetAllAsync"/>
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        try
        {
            var foundEntities = (await _repository.GetAllAsync()).ToArray();
            var foundDtos = _mapper.Map<IEnumerable<ProductDto>>(foundEntities);

            LogGettingAll(foundEntities.Length);
            return foundDtos;
        }
        catch (Exception e)
        {
            LogGettingAllError(e);
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

            LogCreation(createdDto);
            return createdDto;
        }
        catch (Exception e)
        {
            LogCreationError(e, modelToCreate);
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
                LogAnUpdateNotFound(modelToUpdate);
                return null;
            }

            _mapper.Map(modelToUpdate, entityToUpdate);

            var updatedEntity = await _repository.UpdateAsync(entityToUpdate);
            var updatedDto = _mapper.Map<ProductDto>(updatedEntity);

            LogAnUpdate(updatedDto);
            return updatedDto;
        }
        catch (Exception e)
        {
            LogAnUpdateError(e, modelToUpdate);
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
                    LogABulkUpdateNotFound(modelToUpdate);
                    continue;
                }

                LogABulkUpdate(modelToUpdate);

                _mapper.Map(modelToUpdate, entityToUpdate);
                entitiesToUpdate.Add(entityToUpdate);
            }

            await _repository.BulkUpdateAsync(entitiesToUpdate);
        }
        catch (Exception e)
        {
            LogABulkUpdateError(e);
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
                LogDeletionNotFound(id);
                return null;
            }

            var deletedDto = _mapper.Map<ProductDto>(deletedEntity);

            LogDeletion(deletedDto);
            return deletedDto;
        }
        catch (Exception e)
        {
            LogDeletionError(e, id);
            throw;
        }
    }

    #endregion

    #region Logging

    private void LogGetting(ProductDto foundDto)
    {
        _logger.LogDebug(AppLogEvents.Read, "{Title}: id={ProductId}, {Details}",
            "The product was found", foundDto.Id, $"dto={Serialize(foundDto)}");
    }

    private void LogGettingNotFound(long id)
    {
        _logger.LogDebug(AppLogEvents.ReadNotFound, "{Title}: id={ProductId}",
            "The product was not found", id);
    }

    private void LogGettingError(Exception e, long id)
    {
        _logger.LogError(AppLogEvents.Read, e, "{Title}: id={ProductId}",
            "Failed to found the product", id);
    }

    private void LogGettingAll(int count)
    {
        _logger.LogDebug(AppLogEvents.Read, "{Title}: count={ProductCount}",
            "All products was returned", count);
    }

    private void LogGettingAllError(Exception e)
    {
        _logger.LogError(AppLogEvents.Read, e, "{Title}", "Failed to get all products");
    }

    private void LogCreation(ProductDto createdDto)
    {
        _logger.LogDebug(AppLogEvents.Create, "{Title}: id={ProductId}, {Details}",
            "The product was created", createdDto.Id, $"dto={Serialize(createdDto)}");
    }

    private void LogCreationError(Exception e, ProductCreationModel modelToCreate)
    {
        _logger.LogError(AppLogEvents.Create, e, "{Title}: {Details}",
            "Failed to create a product", $"model={Serialize(modelToCreate)}");
    }

    private void LogAnUpdate(ProductDto updatedDto)
    {
        _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
            "The product was updated", updatedDto.Id, $"dto={Serialize(updatedDto)}");
    }

    private void LogAnUpdateNotFound(ProductUpdateModel modelToUpdate)
    {
        _logger.LogDebug(AppLogEvents.UpdateNotFound, "{Title}: id={ProductId}, {Details}",
            "The product to update was not found", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogAnUpdateError(Exception e, ProductUpdateModel modelToUpdate)
    {
        _logger.LogError(AppLogEvents.Update, e, "{Title}: id={ProductId}, {Details}",
            "Failed to update the product", modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogABulkUpdate(ProductUpdateModel modelToUpdate)
    {
        _logger.LogDebug(AppLogEvents.Update, "{Title}: id={ProductId}, {Details}",
            "The product was updated during the bulk update",
            modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogABulkUpdateNotFound(ProductUpdateModel modelToUpdate)
    {
        _logger.LogDebug(AppLogEvents.UpdateNotFound, "{Title}: id={ProductId}, {Details}",
            "The product to update was not found during the bulk update",
            modelToUpdate.Id, $"model={Serialize(modelToUpdate)}");
    }

    private void LogABulkUpdateError(Exception e)
    {
        _logger.LogError(AppLogEvents.Update, e, "{Title}", "Failed to update products");
    }

    private void LogDeletion(ProductDto deletedDto)
    {
        _logger.LogDebug(AppLogEvents.Delete, "{Title}: id={ProductId}, {Details}",
            "The product was deleted", deletedDto.Id, $"dto={Serialize(deletedDto)}");
    }

    private void LogDeletionNotFound(long id)
    {
        _logger.LogDebug(AppLogEvents.DeleteNotFound, "{Title}: id={ProductId}",
            "The product to delete was not found", id);
    }

    private void LogDeletionError(Exception e, long id)
    {
        _logger.LogError(e, "{Title}: id={ProductId}", "Failed to delete the product", id);
    }

    #endregion

    #region Private logic

    private static string Serialize<T>(T source) where T : class
    {
        return StringHelper.Serialize(source);
    }

    #endregion

    #region Mapping rules

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

    #endregion
}