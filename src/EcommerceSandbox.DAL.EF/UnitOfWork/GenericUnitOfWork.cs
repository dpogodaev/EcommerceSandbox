using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;
using EcommerceSandbox.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceSandbox.DAL.EF.UnitOfWork;

///<inheritdoc cref="IGenericUnitOfWork{TContext}"/>
public class GenericUnitOfWork<TContext> : IGenericUnitOfWork<TContext>, IDisposable where TContext : DbContext
{
    private IDbContextTransaction _transaction;
    private Dictionary<string, object> _repositories;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericUnitOfWork{TContext}"/> class with DbContext provided.
    /// </summary>
    public GenericUnitOfWork(TContext context)
    {
        _disposed = false;

        Context = context;
    }

    #region IGenericUnitOfWork

    /// <inheritdoc />
    public TContext Context { get; }

    /// <inheritdoc />
    public void CreateTransaction()
    {
        _transaction = Context.Database.BeginTransaction();
    }

    /// <inheritdoc />
    public void Commit()
    {
        _transaction.Commit();
    }

    /// <inheritdoc />
    public void Rollback()
    {
        _transaction.Rollback();
        _transaction.Dispose();
    }

    /// <inheritdoc />
    public void Save()
    {
        Context.SaveChanges();
    }

    /// <inheritdoc />
    public IGenericRepository<T> GenericRepository<T>() where T : class
    {
        _repositories ??= new Dictionary<string, object>();

        var type = typeof(T).Name;

        if (_repositories.ContainsKey(type))
        {
            return (GenericRepository<T>)_repositories[type];
        }

        var repositoryType = typeof(GenericRepository<T>);
        var repositoryInstance = Activator.CreateInstance(repositoryType, Context);

        _repositories.Add(type, repositoryInstance);

        return (GenericRepository<T>)_repositories[type];
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
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Context.Dispose();
        }

        _disposed = true;
    }

    #endregion
}