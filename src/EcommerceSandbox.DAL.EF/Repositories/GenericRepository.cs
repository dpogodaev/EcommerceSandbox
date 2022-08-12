using System.Linq.Expressions;
using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.DAL.EF.Repositories;

/// <inheritdoc />
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _entities;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class with DbContext provided.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public GenericRepository(DbContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    #region Database

    /// <summary>
    /// Database context.
    /// </summary>
    protected DbContext Context => _context;

    /// <summary>
    /// Database table.
    /// </summary>
    protected virtual IQueryable<T> Table => _entities;

    /// <summary>
    /// Database entities.
    /// </summary>
    protected virtual DbSet<T> Entities => _entities;

    #endregion

    #region IGenericRepository

    /// <inheritdoc />
    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_entities);
    }

    /// <inheritdoc />
    public virtual async Task<T> GetByIdAsync(object id)
    {
        return await _entities.FindAsync(id);
    }

    ///<inheritdoc/>
    public virtual Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate)
    {
        var filteredEntities = _entities.Where(predicate).AsEnumerable();

        return Task.FromResult(filteredEntities);
    }

    /// <inheritdoc />
    public virtual async Task<T> InsertAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return (await _entities.AddAsync(entity)).Entity;
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Remove(entity);

        return Task.CompletedTask;
    }

    #endregion
}