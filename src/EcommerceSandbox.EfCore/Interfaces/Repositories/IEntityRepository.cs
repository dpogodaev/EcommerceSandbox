using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceSandbox.EfCore.Entities;

namespace EcommerceSandbox.EfCore.Interfaces.Repositories;

/// <summary>
/// Defines the methods used to access a specific database entity.
/// </summary>
/// <typeparam name="TEntity">Type of database entry.</typeparam>
/// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
public interface IEntityRepository<TEntity, in TPrimaryKey> where TEntity : PersistentEntity<TPrimaryKey>
{
    /// <summary>
    /// Returns all entities.
    /// </summary>
    /// <returns>SQL query to get all entities.</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Finds an entity by ID.
    /// </summary>
    /// <param name="id">Entity ID to search for.</param>
    /// <returns>The <typeparamref name="TEntity"/> if the entity was found; <c>null</c> otherwise.</returns>
    Task<TEntity> GetByIdAsync(TPrimaryKey id);

    /// <summary>
    /// Returns filtered entities.
    /// </summary>
    /// <param name="predicate">Predicate of filter.</param>
    /// <returns>SQL query for filtering entities.</returns>
    IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The created entity.</returns>
    Task<TEntity> InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The <see cref="Task"/> that will be completed when the entity is updated.</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Removes an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>The <see cref="Task"/> that will be completed when the entity is deleted.</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Marks an entity as unusable, without erasing the data itself from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>The <see cref="Task"/> that will be completed when the entity is marked as deleted.</returns>
    Task SoftDeleteAsync(TEntity entity);
}