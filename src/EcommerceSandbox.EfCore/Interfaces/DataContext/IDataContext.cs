using EcommerceSandbox.EfCore.Entities;
using EcommerceSandbox.EfCore.Interfaces.Repositories;

namespace EcommerceSandbox.EfCore.Interfaces.DataContext;

/// <summary>
/// Provides access to repositories for working with database entities and saving changes to the database.
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// Returns a repository for working with a specific database entity.
    /// </summary>
    /// <typeparam name="TEntity">Type of database entry.</typeparam>
    /// <typeparam name="TPrimaryKey">Type of primary key.</typeparam>
    IEntityRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>()
        where TEntity : PersistentEntity<TPrimaryKey>;

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <remarks>
    /// Transaction will be created automatically if the 'BeginTransaction' method has not been called.
    /// </remarks>
    void SaveChanges();
}