namespace EcommerceSandbox.AppStorages.Interfaces.Repositories;

/// <summary>
/// Provides access to work with database transactions.
/// </summary>
/// <remarks>
/// Using a transaction, we can perform operations with the database, applying the principle of "do everything or do nothing".
/// </remarks>
public interface IUnitOfWork
{
    /// <summary>
    /// Begins a new transaction.
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Commits all changes made to the database in the current transaction.
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// Discards all changes made to the database in the current transaction.
    /// </summary>
    void RollbackTransaction();
}