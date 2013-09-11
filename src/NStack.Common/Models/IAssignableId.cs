#region header
// -----------------------------------------------------------------------
//  <copyright file="IAssignableId.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion
namespace NStack.Models
{
    /// <summary>
    /// A contract for types that allow the id to be assigned.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public interface IAssignableId<in TId>
    {
        /// <summary>
        /// Assigns an identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        void AssignId(TId id);
    }
}