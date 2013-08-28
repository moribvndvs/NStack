#region header

// -----------------------------------------------------------------------
//  <copyright file="IRepository[TEntity].cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

namespace NStack.Data
{
    /// <summary>
    /// A contract implemented by respositories of entities that use the <see cref="Flake"/> identifier.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Flake>
    {
    }
}