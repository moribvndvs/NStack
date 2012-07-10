#region header
// <copyright file="ModelMapperExtensions.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
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
            Requires.That(inspector, "inspector").IsNotNull();
            Requires.That(type, "type").IsNotNull();

            PropertyInfo property = (from prop in type.GetProperties()
                                     where inspector.IsPersistentId(prop)
                                     select prop).FirstOrDefault();

            return property == null ? null : new PropertyPath(null, property);

        }
    }
}