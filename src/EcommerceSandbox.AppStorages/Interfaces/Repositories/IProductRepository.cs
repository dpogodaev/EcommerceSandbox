using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.AppStorages.Dtos;
using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.AppStorages.Interfaces.Repositories;

/// <summary>
/// Persistent repository for <see cref="Product"/>s.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Finds a <see cref="Product"/> entity in the storage by ID.
    /// </summary>
    /// <param name="id">Entity ID to search for.</param>
    /// <returns>The <see cref="ProductDto"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<ProductDto> GetByIdAsync(long id);

    //TODO: to del
    IEnumerable<ProductDto> GetAll();

    /// <summary>
    /// Creates a new <see cref="Product"/> entity in the storage.
    /// </summary>
    /// <param name="dtoToCreate">The <see cref="ProductDto"/> to create an entity.</param>
    /// <returns>The <see cref="ProductDto"/> of the created entity.</returns>
    Task<ProductDto> CreateAsync(ProductDto dtoToCreate);

    /// <summary>
    /// Updates an existing <see cref="Product"/> entity in the storage.
    /// </summary>
    /// <param name="dtoToUpdate">The <see cref="ProductDto"/> to update the entity.</param>
    /// <returns>
    /// The <see cref="ProductDto"/> of the updated entity, if the entity was found; <c>null</c> otherwise.
    /// </returns>
    Task<ProductDto> UpdateAsync(ProductDto dtoToUpdate);

    /// <summary>
    /// Updates existing <see cref="Product"/> entities in the storage.
    /// </summary>
    /// <param name="dtosToUpdate"><see cref="ProductDto"/>s to update entities.</param>
    /// <returns>The <see cref="Task"/> that will be completed when all entities are updated.</returns>
    Task BulkUpdateAsync(IEnumerable<ProductDto> dtosToUpdate);

    /// <summary>
    /// Deletes a <see cref="Product"/> entity from the storage.
    /// </summary>
    /// <param name="id">Entity ID to delete.</param>
    /// <returns>
    /// The <see cref="ProductDto"/> of the deleted entity, if the entity was found; <c>null</c> otherwise.
    /// </returns>
    Task<ProductDto> DeleteAsync(long id);

    /// <summary>
    /// Marks a <see cref="Product"/> entity as deleted in the storage.
    /// </summary>
    /// <param name="id">Entity ID to delete.</param>
    /// The <see cref="ProductDto"/> of the marked entity, if the entity was found; <c>null</c> otherwise.
    Task<ProductDto> SoftDeleteAsync(long id);
}