#region header

// -----------------------------------------------------------------------
//  <copyright file="Repository[TEntity,TId].cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NStack.Data
{
    /// <summary>
    ///     A generic base class for implementing <see cref="IRepository{TEntity,TId}" /> .
    /// </summary>
    /// <typeparam name="TEntity"> The type of the entity. </typeparam>
    /// <typeparam name="TId"> The type of the entity's ID property. </typeparam>
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    {
        
       /// <summary>
        ///     When implemented, creates a new <see cref="IQueryable{TEntity}" />.
        /// </summary>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> Query();

        #region Implementation of IRepository<TEntity,in TId>

        /// <summary>
        ///     Refreshes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Refresh(TEntity entity);

        /// <summary>
        ///     Gets the <see cref="IUnitOfWork" /> responsible for managing unit of work transactions.
        /// </summary>
        public abstract IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity"> The entity to attach. </param>
        public abstract void Add(TEntity entity);

        /// <summary>
        ///     Returns the entity with the specified <paramref name="id" /> from the repository.
        /// </summary>
        /// <param name="id"> The ID of the desired entity. </param>
        /// <returns>
        ///     The entity with the specified <paramref name="id" /> ; otherwise, null.
        /// </returns>
        public abstract TEntity Get(TId id);

        /// <summary>
        ///     Returns an instance of the entity with the specified <paramref name="id" /> , which can be used in operations where the object does not have to be fully loaded from the underlying store.
        /// </summary>
        /// <param name="id"> The ID of the desired entity. </param>
        /// <returns> The entity with the specified ID. </returns>
        /// <remarks>
        ///     During certain operations, it is not always useful or efficient to completely load an entity from the underlying store. For example, if you wish to remove an entity from the repository or use it to establish a relationship, you do not necessarily need the entire entity loaded from the underlying store. Instead a deferred or proxied instance of the entity, where at least the ID property is set, can be used to efficiently complete the operation. Preferrably, repositories should return an object that when any other member on the returned entity than Id, GetHashCode, or Equals is invoked, it should then load the object from the underlying store before continuing. DeferredGet differs from
        ///     <see
        ///         cref="IRepository{TEntity,TId}.Get" />
        ///     , in that <see cref="IRepository{TEntity,TId}.Get" /> should always return the entity after retrieving it from the underlying store. Finally, if this method is supported by the implementation, it should always return an entity with the specified ID set, even if an entity with that ID does not exist in the repository. The repository should wait to check for the existence of the specified entity when changes are actually made to the repository (such as when an item is deleted).
        /// </remarks>
        /// <exception cref="NotSupportedException">Deferred loading is not supported by the implementation.</exception>
        public abstract TEntity DeferredGet(TId id);

        /// <summary>
        ///     Removes the specified entity from the repository.
        /// </summary>
        /// <param name="entity"> The entity to remove. </param>
        public abstract void Remove(TEntity entity);

        /// <summary>
        ///     Returns the total number of entities in the repository.
        /// </summary>
        /// <returns> The total number of entities in the repository. </returns>
        public abstract int Count();

        /// <summary>
        ///     Ensures the repository watches for changes to the entity so changes can be persisted.
        /// </summary>
        /// <param name="entity"> The entity/ </param>
        public abstract void Attach(TEntity entity);

        /// <summary>
        ///     Prevents the repository from watching for changes to the entity, so the entity can be modified without changes being persisted.
        /// </summary>
        /// <param name="entity"> </param>
        public abstract void Detach(TEntity entity);

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Query().GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        /// <summary>
        ///     Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of
        ///     <see
        ///         cref="T:System.Linq.IQueryable" />
        ///     .
        /// </returns>
        public Expression Expression
        {
            get { return Query().Expression; }
        }

        /// <summary>
        ///     Gets the type of the element(s) that are returned when the expression tree associated with this instance of
        ///     <see
        ///         cref="T:System.Linq.IQueryable" />
        ///     is executed.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { return Query().ElementType; }
        }

        /// <summary>
        ///     Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider
        {
            get { return Query().Provider; }
        }

        #endregion
    }
}