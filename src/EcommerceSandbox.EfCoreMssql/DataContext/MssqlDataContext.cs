using EcommerceSandbox.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.EfCoreMssql.DataContext;

/// <summary>
/// MS SQL implementation of database context.
/// </summary>
public class MssqlDataContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MssqlDataContext"/> class with the options provided.
    /// </summary>
    /// <param name="options">Database context options for connect to the database server.</param>
    public MssqlDataContext(DbContextOptions<MssqlDataContext> options) : base(options)
    {
    }

    #region Database tables

    public DbSet<Product> Products { get; set; }

    #endregion
}