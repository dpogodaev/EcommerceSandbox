﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.AppServices.Dtos;
using EcommerceSandbox.AppServices.Models.Product;
using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppServices.Interfaces.Services;

/// <summary>
/// Application service for performing operations with <see cref="Product"/>s.
/// </summary>
public interface IProductAppService
{
    /// <summary>
    /// Finds a <see cref="Product"/> entity by ID.
    /// </summary>
    /// <param name="id">Entity ID to search for.</param>
    /// <returns>The <see cref="ProductDto"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<ProductDto> GetByIdAsync(long id);

    //TODO: to del
    IEnumerable<ProductDto> GetAll();

    /// <summary>
    /// Creates a new <see cref="Product"/> entity.
    /// </summary>
    /// <param name="modelToCreate">The <see cref="ProductCreationModel"/> to create an entity.</param>
    /// <returns>The <see cref="ProductDto"/> of the created entity.</returns>
    Task<ProductDto> CreateAsync(ProductCreationModel modelToCreate);

    /// <summary>
    /// Updates an existing <see cref="Product"/> entity.
    /// </summary>
    /// <param name="modelToUpdate">The <see cref="ProductUpdateModel"/> to update the entity.</param>
    /// <returns>
    /// The <see cref="ProductDto"/> of the updated entity, if the entity was found; <c>null</c> otherwise.
    /// </returns>
    Task<ProductDto> UpdateAsync(ProductUpdateModel modelToUpdate);

    /// <summary>
    /// Updates existing <see cref="Product"/> entities.
    /// </summary>
    /// <param name="modelsToUpdate"><see cref="ProductUpdateModel"/>s to update entities.</param>
    /// <returns>The <see cref="Task"/> that will be completed when all entities are updated.</returns>
    Task BulkUpdateAsync(IEnumerable<ProductUpdateModel> modelsToUpdate);

    /// <summary>
    /// Deletes a <see cref="Product"/> entity.
    /// </summary>
    /// <param name="id">Entity ID to delete.</param>
    /// <returns>The <see cref="ProductDto"/> of the deleted entity.</returns>
    Task<ProductDto> DeleteAsync(long id);
}