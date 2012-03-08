#region header

// <copyright file="IRepository.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;
using System.Collections.Generic;

namespace MG.Persistence
{
    /// <summary>
    /// A contract implemented by classes that act as a repository of objects.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity the repository contains.</typeparam>
    /// <typeparam name="TId">The type of the identifier property on the entity, used to identify a particular instance. </typeparam>
    public interface IRepository<TEntity, in TId> : IEnumerable<TEntity>
    {
        /// <summary>
        /// Attaches the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Returns the entity with the specified <paramref name="id"/> from the repository.
        /// </summary>
        /// <param name="id">The ID of the desired entity.</param>
        /// <returns>The entity with the specified <paramref name="id"/>; otherwise, null.</returns>
        TEntity Get(TId id);

        /// <summary>
        /// Returns an instance of the entity with the specified <paramref name="id"/>, which can be used in operations where the object does not have to be fully loaded from the underlying store.
        /// </summary>
        /// <param name="id">The ID of the desired entity.</param>
        /// <returns>The entity with the specified ID.</returns>
        /// <remarks>
        /// During certain operations, it is not always useful or efficient to completely load an entity from the underlying store. For example, if you wish to remove an entity from the repository
        /// or use it to establish a relationship, you do not necessarily need the entire entity loaded from the underlying store. Instead a deferred or proxied instance of the entity, where 
        /// at least the ID property is set, can be used to efficiently complete the operation. Preferrably, repositories should return an object that when any other member on the returned entity
        /// than Id, GetHashCode, or Equals is invoked, it should then load the object from the underlying store before continuing.
        /// 
        /// DeferredGet differs from <see cref="Get"/>, in that <see cref="Get"/> should always return the entity after retrieving it from the underlying store.
        /// 
        /// Finally, if this method is supported by the implementation, it should always return an entity with the specified ID set, even if an entity with that ID does not exist in the repository.
        /// The repository should wait to check for the existence of the specified entity when changes are actually made to the repository (such as when an item is deleted).
        /// </remarks>
        /// <exception cref="NotSupportedException">Deferred loading is not supported by the implementation.</exception>
        TEntity DeferredGet(TId id);

        /// <summary>
        /// Removes the specified entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Returns whether or not the specified entity exists in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if the entity exists in the repository; otherwise, false.</returns>
        bool Contains(TEntity entity);

        /// <summary>
        /// Returns the total number of entities in the repository.
        /// </summary>
        /// <returns>The total number of entities in the repository.</returns>
        int Count();

        /// <summary>
        /// Ensures the repository watches for changes to the entity so changes can be persisted.
        /// </summary>
        /// <param name="entity">The entity/</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Prevents the repository from watching for changes to the entity, so the entity can be modified without changes being persisted.
        /// </summary>
        /// <param name="entity"></param>
        void Detach(TEntity entity);
    }
}