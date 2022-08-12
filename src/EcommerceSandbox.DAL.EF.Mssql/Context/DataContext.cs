using EcommerceSandbox.DAL.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSandbox.DAL.EF.Mssql.Context;

/// <summary>
/// MS SQL implementation of database context.
/// </summary>
public class DataContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataContext"/> class with the options provided.
    /// </summary>
    /// <param name="options">Database context options.</param>
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    #region Database entities

    public DbSet<Product> Products { get; set; }

    #endregion
}