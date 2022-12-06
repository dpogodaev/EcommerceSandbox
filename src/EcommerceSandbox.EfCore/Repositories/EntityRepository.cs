using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EcommerceSandbox.EfCore.Entities;
using EcommerceSandbox.EfCore.Interfaces.Entities;
using EcommerceSandbox.EfCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.EfCore.Repositories;

/// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}"/>
public class EntityRepository<TEntity, TPrimaryKey> : IEntityRepository<TEntity, TPrimaryKey>
    where TEntity : PersistentEntity<TPrimaryKey>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _entities;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityRepository{TEntity,TPrimaryKey}"/> class with database context provided.
    /// </summary>
    /// <param name="context">Database context.</param>
    public EntityRepository(DbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    #region IEntityRepository

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.GetAll"/>
    public IQueryable<TEntity> GetAll()
    {
        return _entities;
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.GetByIdAsync"/>
    public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
    {
        return await _entities.FindAsync(id);
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.Filter"/>
    public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Where(predicate).AsQueryable();
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.InsertAsync"/>
    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        SetCreationTimeChanges(entity);

        return (await _entities.AddAsync(entity)).Entity;
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.UpdateAsync"/>
    public Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        IgnoreCreationTimeChanges(entity);
        IgnoreDeletionTimeChanges(entity);
        SetModificationTimeChanges(entity);

        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.DeleteAsync"/>
    public Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Remove(entity);

        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IEntityRepository{TEntity,TPrimaryKey}.SoftDeleteAsync"/>
    public Task SoftDeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        SetDeletionTimeChanges(entity);

        return Task.CompletedTask;
    }

    #endregion

    #region Private methods

    private void IgnoreCreationTimeChanges(TEntity entity)
    {
        if (entity is IHasCreationTime e)
        {
            _context.Entry(e).Property(x => x.CreationTime).IsModified = false;
        }
    }

    private void IgnoreDeletionTimeChanges(TEntity entity)
    {
        if (entity is IHasDeletionTime e)
        {
            _context.Entry(e).Property(x => x.DeletionTime).IsModified = false;
        }
    }

    private static void SetCreationTimeChanges(TEntity entity)
    {
        if (entity is IHasCreationTime e)
        {
            e.CreationTime = DateTime.UtcNow;
        }
    }

    private void SetModificationTimeChanges(TEntity entity)
    {
        if (entity is not IHasModificationTime e) return;

        _context.Entry(e).Property(x => x.LastModificationTime).IsModified = false;

        if (_context.Entry(entity).State == EntityState.Modified)
        {
            e.LastModificationTime = DateTime.UtcNow;
        }
    }

    private static void SetDeletionTimeChanges(TEntity entity)
    {
        if (entity is not IHasDeletionTime e) return;

        e.DeletionTime = DateTime.UtcNow;
        e.IsDeleted = true;
    }

    #endregion
}