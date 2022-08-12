using EcommerceSandbox.DAL.EF.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.DAL.EF.Interfaces.UnitOfWork;

/// <summary>
/// Generic Unit of work in Repository pattern.
/// </summary>
/// <remarks>Manages operations with objects in the in-memory database as a single transaction.</remarks>
public interface IGenericUnitOfWork<out TContext> where TContext : DbContext
{
    /// <summary>
    /// Returns the DBContext object.
    /// </summary>
    TContext Context { get; }

    /// <summary>
    /// Creates a database transaction.
    /// </summary>
    /// <remarks>
    /// Using a transaction, we can perform operations with the database,
    /// applying the principle of "do everything and do nothing".
    /// </remarks>
    void CreateTransaction();

    /// <summary>
    /// Implements the SaveChanges method of the DbContext class.
    /// </summary>
    /// <remarks>
    /// Whenever we do a transaction, we need to call this method so that it will makes changes in the database.
    /// </remarks>
    void Save();

    /// <summary>
    /// Saves the changes permanently in the database.
    /// </summary>
    /// <remarks>If all transactions are completed successfully, then we need to call this method.</remarks>
    void Commit();

    /// <summary>
    /// Rolls back the database changes to its previous state.
    /// </summary>
    /// <remarks>If at least one transaction failed, we need to call this method.</remarks>
    void Rollback();

    /// <summary>
    /// Returns a generic repository for specific class of database entry.
    /// </summary>
    /// <typeparam name="T">Database entry class.</typeparam>
    /// <returns>Generic repository.</returns>
    IGenericRepository<T> GenericRepository<T>() where T : class;
}