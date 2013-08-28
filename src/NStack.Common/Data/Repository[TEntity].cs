#region header
// -----------------------------------------------------------------------
//  <copyright file="Repository[TEntity].cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion
namespace NStack.Data
{
    /// <summary>
    /// A generic base class for repositories that implement <see cref="IRepository{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> : Repository<TEntity, Flake>, IRepository<TEntity>
    {
         
    }
}