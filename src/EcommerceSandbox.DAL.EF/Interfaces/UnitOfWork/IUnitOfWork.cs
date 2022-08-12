using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;

/// <summary>
/// Unit of work in Repository pattern.
/// </summary>
public interface IUnitOfWork
{
    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Context"/>
    public DbContext Context { get; }

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.CreateTransaction"/>
    public void CreateTransaction();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Commit"/>
    public void Commit();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Rollback"/>
    public void Rollback();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.Save"/>
    public void Save();

    /// <inheritdoc cref="IGenericUnitOfWork{TContext}.GenericRepository{T}"/>
    public IGenericRepository<T> GenericRepository<T>() where T : class;
}