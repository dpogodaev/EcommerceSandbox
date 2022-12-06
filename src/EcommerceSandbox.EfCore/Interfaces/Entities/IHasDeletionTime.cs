using System;

namespace EcommerceSandbox.EfCore.Interfaces.Entities;

/// <summary>
/// An entity can implement this interface if <see cref="DeletionTime"/> of this entity must be stored.
/// <see cref="DeletionTime"/> is automatically set when deleting entity.
/// </summary>
public interface IHasDeletionTime : ISoftDelete
{
    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    DateTime? DeletionTime { get; set; }
}