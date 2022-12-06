using System;
using EcommerceSandbox.AppStorages.Interfaces.Repositories;
using EcommerceSandbox.EfCore.Entities;
using EcommerceSandbox.EfCore.Interfaces.DataContext;
using EcommerceSandbox.EfCore.Interfaces.Repositories;
using EcommerceSandbox.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceSandbox.EfCore.DataContext;

/// <summary>
/// Entity Framework implementation of <see cref="IDataContext"/> and <see cref="IUnitOfWork"/>.
/// </summary>
public class DataContextFacade<TContext> : IDataContext, IUnitOfWork, IDisposable where TContext : DbContext
{
    private readonly TContext _context;

    private IDbContextTransaction _transaction;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataContextFacade{TContext}"/> class with database context provided.
    /// </summary>
    /// <param name="context">Database context.</param>
    public DataContextFacade(TContext context)
    {
        _context = context;
    }

    #region IDataContext

    /// <inheritdoc cref="IDataContext.SaveChanges"/>
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    /// <inheritdoc cref="IDataContext.GetRepository{TEntity,TPrimaryKey}"/>
    public IEntityRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>()
        where TEntity : PersistentEntity<TPrimaryKey>
    {
        return new EntityRepository<TEntity, TPrimaryKey>(_context);
    }

    #endregion

    #region IUnitOfWork

    /// <inheritdoc cref="IUnitOfWork.BeginTransaction"/>
    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    /// <inheritdoc cref="IUnitOfWork.CommitTransaction"/>
    public void CommitTransaction()
    {
        _transaction.Commit();
    }

    /// <inheritdoc cref="IUnitOfWork.RollbackTransaction"/>
    public void RollbackTransaction()
    {
        _transaction.Rollback();
        _transaction.Dispose();
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Public implementation of Dispose pattern callable by consumers.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    /// <param name="disposing">
    /// Indicates if the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).
    /// </param>
    protected void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }

    #endregion
}