#region header
// -----------------------------------------------------------------------
//  <copyright file="ModelMapperExtensions.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.Linq;
using System.Reflection;

using NHibernate.Mapping.ByCode;

namespace NStack.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelMapperExtensions
    {
        /// <summary>
        /// Returns the <see cref="PropertyPath"/> that is the entity's persistent ID.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="type">The entity type.</param>
        /// <returns>The <see cref="PropertyPath"/> to the persistent ID, or null if one was not found.</returns>
        public static PropertyPath FindPersistentId(this IModelInspector inspector, Type type)
        {
            Requires.That(inspector).IsNotNull();
            Requires.That(type).IsNotNull();

            PropertyInfo property = (from prop in type.GetProperties()
                                     where inspector.IsPersistentId(prop)
                                     select prop).FirstOrDefault();

            return property == null ? null : new PropertyPath(null, property);

        }
    }
}