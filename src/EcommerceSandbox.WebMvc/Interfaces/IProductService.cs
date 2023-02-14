using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.DomainEntities.Entities;
using EcommerceSandbox.WebMvc.Dtos;
using EcommerceSandbox.WebMvc.Models.Product;

namespace EcommerceSandbox.WebMvc.Interfaces;

/// <summary>
/// Service for performing operations with <see cref="Product"/>s.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Finds a <see cref="Product"/> entity by ID.
    /// </summary>
    /// <param name="id">Entity ID to search for.</param>
    /// <returns>The <see cref="ProductDto"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<ProductDto> GetByIdAsync(long id);

    /// <summary>
    /// Returns a list of all <see cref="Product"/>s.
    /// </summary>
    /// <returns><see cref="ProductDto"/>s of all found entities.</returns>
    Task<IEnumerable<ProductDto>> GetAllAsync();

    /// <summary>
    /// Adds a new <see cref="Product"/> entity.
    /// </summary>
    /// <param name="modelToCreate">The <see cref="ProductCreationModel"/> to create an entity.</param>
    /// <returns>The <see cref="ProductDto"/> of the created entity.</returns>
    Task<ProductDto> AddAsync(ProductCreationModel modelToCreate);

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