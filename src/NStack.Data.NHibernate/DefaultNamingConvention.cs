#region header

// <copyright file="DefaultNamingConvention.cs" company="mikegrabski.com">
//    Copyright 2013 Mike Grabski
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

using NHibernate.Mapping.ByCode;

using NStack.Extensions;

namespace NStack.Data
{
    /// <summary>
    ///     The default implementation of <see cref="INamingConvention" />.
    /// </summary>
    public class DefaultNamingConvention : INamingConvention
    {
        private const string KeyColumnFormat = "{0}_{1}";

        private const string ForeignKeyNameFormat = "fk_{0}_{1}_{2}";

        private const string IndexNameFormat = "ix_{0}_{1}";

        #region INamingConvention Members

        /// <summary>
        ///     Returns the name of the table for the model.
        /// </summary>
        /// <param name="modelInspector">The model inspector.</param>
        /// <param name="type">The model type.</param>
        /// <returns>The name of the model's table.</returns>
        public string Table(IModelInspector modelInspector, Type type)
        {
            Requires.That(type, "type").IsNotNull();

            return type.Name.Pluralize().Underscore();
        }

        /// <summary>
        ///     Returns the name of the table column for an entity's property.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="member">The entity property.</param>
        /// <param name="declaringType"> </param>
        /// <returns>The name of the table column.</returns>
        public string Column(IModelInspector inspector, PropertyPath member, Type declaringType = null)
        {
            Requires.That(member, "member").IsNotNull();
            Requires.That(member.LocalMember, "member.LocalMember").IsNotNull();

            string localName = member.LocalMember.Name.Underscore();

            if (declaringType != null)
            {
                return KeyColumnFormat.Formatted(declaringType.Name.Underscore(), localName);
            }

            Type type = member.LocalMember.GetPropertyOrFieldType();

            if (inspector.IsEntity(type)) // is a foreign key
            {
                PropertyPath id = inspector.FindPersistentId(type);

                if (id != null) return KeyColumnFormat.Formatted(localName, Column(inspector, id));
            }

            return localName;
        }


        /// <summary>
        ///     Returns the name of the foreign key in a relationship.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="member">The entity property.</param>
        /// <returns>The name of the foreign key.</returns>
        public string ForeignKey(IModelInspector inspector, PropertyPath member, Type declaringType = null,
                                 Type idDeclaringType = null)
        {
            Requires.That(member, "member").IsNotNull();
            Requires.That(member.LocalMember, "member.LocalMember").IsNotNull();

            return
                ForeignKeyNameFormat.Formatted(
                    Table(inspector, idDeclaringType ?? member.LocalMember.GetPropertyOrFieldType()),
                    Table(inspector, declaringType ?? member.LocalMember.DeclaringType),
                    Column(inspector, member, idDeclaringType));
        }

        /// <summary>
        ///     Returns the name of the index for the column.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="member">The entity property.</param>
        /// <returns>The name of the index.</returns>
        public string Index(IModelInspector inspector, PropertyPath member)
        {
            Requires.That(member, "member").IsNotNull();
            Requires.That(member.LocalMember, "member.LocalMember").IsNotNull();

            return IndexNameFormat.Formatted(Table(inspector, member.LocalMember.DeclaringType),
                                             Column(inspector, member));
        }

        /// <summary>
        ///     Returns the name of a key column.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="member"></param>
        /// <param name="declaringType"> </param>
        /// <returns></returns>
        public string KeyColumn(IModelInspector inspector, PropertyPath member, Type declaringType = null)
        {
            Requires.That(member, "member").IsNotNull();
            Requires.That(member.LocalMember, "member.LocalMember").IsNotNull();

            Type type = declaringType ?? member.LocalMember.DeclaringType;
            string localName = type.Name.Underscore();
            PropertyPath id = inspector.FindPersistentId(type);

            return KeyColumnFormat.Formatted(localName, id.LocalMember.Name.Underscore());
        }

        /// <summary>
        ///     Returns the name of the index column in an ordered collection.
        /// </summary>
        /// <param name="inspector">The model inspector.</param>
        /// <param name="member">The collection property.</param>
        /// <returns>The name of the index column.</returns>
        public string IndexColumn(IModelInspector inspector, PropertyPath member)
        {
            if (inspector.IsList(member.LocalMember)) return "list_index";

            return "item_index";
        }

        #endregion
    }
}