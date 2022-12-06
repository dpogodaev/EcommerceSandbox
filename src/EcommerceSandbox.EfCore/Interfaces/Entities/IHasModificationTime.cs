using System;

namespace EcommerceSandbox.EfCore.Interfaces.Entities;

/// <summary>
/// An entity can implement this interface if <see cref="LastModificationTime"/> of this entity must be stored.
/// <see cref="LastModificationTime"/> is automatically set when updating entity.
/// </summary>
public interface IHasModificationTime
{
    /// <summary>
    /// The last modified time for this entity.
    /// </summary>
    DateTime? LastModificationTime { get; set; }
}