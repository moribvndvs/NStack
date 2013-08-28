#region header
// -----------------------------------------------------------------------
//  <copyright file="InMemoryRepository[TEntity].cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System.Collections.Generic;

using NStack.Models;

namespace NStack.Data
{
    /// <summary>
    /// A generic, in-memory repository with a <see cref="Flake"/> identifier.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public class InMemoryRepository<TEntity> : InMemoryRepository<TEntity, Flake>
        where TEntity : Entity<Flake>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InMemoryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="items">The list to use as the backing list of the repository.</param>
        public InMemoryRepository(IList<TEntity> items = null) : base(items)
        {
        }
    }
}