using EcommerceSandbox.DAL.EF.Mssql.Context;
using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;
using EcommerceSandbox.DAL.EF.UnitOfWork;

namespace EcommerceSandbox.DAL.EF.Mssql.UnitOfWork;

/// <summary>
/// MS SQL implementation of Unit of work.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly GenericUnitOfWork<DataContext> _genericUnitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class with DbContext provided.
    /// </summary>
    /// <param name="dataContext">The <see cref="DataContext"/>.</param>
    public UnitOfWork(DataContext dataContext)
    {
        _genericUnitOfWork = new GenericUnitOfWork<DataContext>(dataContext);
    }

    #region IUnitOfWork

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Context"/>
    public Microsoft.EntityFrameworkCore.DbContext Context => _genericUnitOfWork.Context;

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.CreateTransaction"/>
    public void CreateTransaction() => _genericUnitOfWork.CreateTransaction();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Commit"/>
    public void Commit() => _genericUnitOfWork.Commit();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Rollback"/>
    public void Rollback() => _genericUnitOfWork.Rollback();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Save"/>
    public void Save() => _genericUnitOfWork.Save();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.GenericRepository{T}"/>
    public IGenericRepository<T> GenericRepository<T>() where T : class => _genericUnitOfWork.GenericRepository<T>();

    #endregion
}