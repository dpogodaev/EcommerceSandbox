using System;
using EcommerceSandbox.EfCore.Interfaces.Entities;
using AppLayerEntities = EcommerceSandbox.DomainEntities.Entities;

namespace EcommerceSandbox.EfCore.Entities;

/// <summary>
/// Persistent entity for <see cref="AppLayerEntities.Product"/>.
/// </summary>
public class Product : PersistentEntity<long>, IHasCreationTime, IHasModificationTime
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }

    /// <inheritdoc cref="IHasCreationTime.CreationTime"/>
    public DateTime CreationTime { get; set; }

    /// <inheritdoc cref="IHasModificationTime.LastModificationTime"/>
    public DateTime? LastModificationTime { get; set; }
}