using System;

namespace EcommerceSandbox.EfCore.Interfaces.Entities;

/// <summary>
/// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
/// <see cref="CreationTime"/> is automatically set when saving entity to database.
/// </summary>
public interface IHasCreationTime
{
    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    DateTime CreationTime { get; set; }
}