#region header

// <copyright file="RootEntity[TId].cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

namespace MG.Models
{
    /// <summary>
    /// A generic root entity with a strongly typed ID.
    /// </summary>
    /// <typeparam name="TId">The type of the ID property.</typeparam>
    public abstract class RootEntity<TId> : Entity
    {
        /// <summary>
        /// Gets the ID of the entity.
        /// </summary>
        public virtual TId Id { get; protected set; }
    }
}