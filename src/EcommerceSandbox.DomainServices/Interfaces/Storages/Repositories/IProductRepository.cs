using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.DomainServices.Interfaces.Storages.Repositories;

/// <summary>
/// Repository for <see cref="Product"/>s.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Finds a <see cref="Product"/> in the storage by ID.
    /// </summary>
    /// <param name="id">Entity ID to search for.</param>
    /// <returns>The <see cref="Product"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<Product> GetByIdAsync(long id);

    //TODO: to del
    IEnumerable<Product> GetAll();

    /// <summary>
    /// Creates a new <see cref="Product"/> in the storage.
    /// </summary>
    /// <param name="entityToCreate">The <see cref="Product"/> to create.</param>
    /// <returns>The created <see cref="Product"/>.</returns>
    Task<Product> CreateAsync(Product entityToCreate);

    /// <summary>
    /// Updates an existing <see cref="Product"/> in the storage.
    /// </summary>
    /// <param name="entityToUpdate">The <see cref="Product"/> to update.</param>
    /// <returns>The updated <see cref="Product"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<Product> UpdateAsync(Product entityToUpdate);

    /// <summary>
    /// Updates existing <see cref="Product"/>s in the storage.
    /// </summary>
    /// <param name="entitiesToUpdate"><see cref="Product"/>s to update.</param>
    /// <returns>The <see cref="Task"/> that will be completed when all <see cref="Product"/>s are updated.</returns>
    Task BulkUpdateAsync(IEnumerable<Product> entitiesToUpdate);

    /// <summary>
    /// Deletes a <see cref="Product"/> from the storage.
    /// </summary>
    /// <param name="id">Entity ID to delete.</param>
    /// <returns>The deleted <see cref="Product"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<Product> DeleteAsync(long id);
}