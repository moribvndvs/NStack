#region header

// -----------------------------------------------------------------------
//  <copyright file="NHRepository[TEntity,TId].cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Linq;

using NHibernate;

using NHibernate.Linq;

namespace NStack.Data
{
    /// <summary>
    /// A generic implementation of <see cref="Repository{TEntity, TId}"/> that wraps NHibernate <see cref="ISession"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TId">The entity's ID type.</typeparam>
    public class NHRepository<TEntity, TId> : Repository<TEntity, TId>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NHRepository{TEntity, TId}"/>.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public NHRepository(NHUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly NHUnitOfWork _unitOfWork;

        /// <summary>
        /// Gets the <see cref="ISession"/> used by the repository.
        /// </summary>
        protected ISession Session
        {
            get { return _unitOfWork.GetSession(); }
        }

        #region Overrides of Repository<TEntity,TId>

        /// <summary>
        /// When implemented, creates a new <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<TEntity> Query()
        {
            return Session.Query<TEntity>();
        }

        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> responsible for managing unit of work transactions.
        /// </summary>
        public override IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        /// <summary>
        ///     Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity"> The entity to attach. </param>
        public override void Add(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
        }

        /// <summary>
        ///     Returns the entity with the specified <paramref name="id" /> from the repository.
        /// </summary>
        /// <param name="id"> The ID of the desired entity. </param>
        /// <returns>
        ///     The entity with the specified <paramref name="id" /> ; otherwise, null.
        /// </returns>
        public override TEntity Get(TId id)
        {
            return Session.Get<TEntity>(id);
        }

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
        public override TEntity DeferredGet(TId id)
        {
            return Session.Load<TEntity>(id);
        }

        /// <summary>
        ///     Removes the specified entity from the repository.
        /// </summary>
        /// <param name="entity"> The entity to remove. </param>
        public override void Remove(TEntity entity)
        {
            Session.Delete(entity);
        }

        /// <summary>
        ///     Returns the total number of entities in the repository.
        /// </summary>
        /// <returns> The total number of entities in the repository. </returns>
        public override int Count()
        {
            return Query().Count();
        }

        /// <summary>
        ///     Ensures the repository watches for changes to the entity so changes can be persisted.
        /// </summary>
        /// <param name="entity"> The entity/ </param>
        public override void Attach(TEntity entity)
        {
            Session.Update(entity);
        }

        /// <summary>
        ///     Prevents the repository from watching for changes to the entity, so the entity can be modified without changes being persisted.
        /// </summary>
        /// <param name="entity"> </param>
        public override void Detach(TEntity entity)
        {
            Session.Evict(entity);
        }

        /// <summary>
        /// Refreshes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Refresh(TEntity entity)
        {
            Session.Refresh(entity, LockMode.None);
        }

        #endregion
    }
}