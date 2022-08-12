using System.Linq.Expressions;

namespace EcommerceSandbox.DAL.EF.Interfaces.Repositories;

/// <summary>
/// Generic repository pattern.
/// </summary>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Returns all entities.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Returns an entity by Id.
    /// </summary>
    /// <param name="id">Entity ID.</param>
    Task<T> GetByIdAsync(object id);

    /// <summary>
    /// Returns filtered entities.
    /// </summary>
    /// <param name="predicate">Predicate of filter.</param>
    Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    Task<T> InsertAsync(T entity);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Removes an entity.
    /// </summary>
    /// <param name="entity">Entity to delete.</param>
    Task DeleteAsync(T entity);
}