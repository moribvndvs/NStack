#region header

// <copyright file="AutoMapper.cs" company="mikegrabski.com">
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

using NStack.Extensions;
using NStack.Models;

namespace NStack.Data
{
    /// <summary>
    ///   Automatically maps persistable entities using conventions.
    /// </summary>
    public class AutoMapper
    {
        /// <summary>
        ///   Initializes a new instance of <see cref="AutoMapper" /> .
        /// </summary>
        public AutoMapper() : this(new DefaultNamingConvention())
        {
        }

        /// <summary>
        ///   Initializes a new instance of <see cref="AutoMapper" /> .
        /// </summary>
        public AutoMapper(INamingConvention namingConvention)
        {
            NamingConvention = namingConvention;
            EntityBaseType = typeof (Entity);
            Mappings = new List<HbmMapping>();

            Initialize();
        }

        /// <summary>
        ///   Gets the <see cref="INamingConvention" /> that will be used to derive names.
        /// </summary>
        protected INamingConvention NamingConvention { get; private set; }

        /// <summary>
        ///   Gets the <see cref="ModelMapper" /> .
        /// </summary>
        protected ModelMapper ModelMapper { get; private set; }

        /// <summary>
        ///   Gets or sets the base type for all entities that should be mapped.
        /// </summary>
        public Type EntityBaseType { get; set; }

        /// <summary>
        ///   Gets a list of all mappings that have been added thus far.
        /// </summary>
        protected IList<HbmMapping> Mappings { get; private set; }

        private void Initialize()
        {
            ModelMapper = CreateModelMapper();
        }

        /// <summary>
        ///   Creates and initializes the model mapper.
        /// </summary>
        /// <returns> The model mapper. </returns>
        protected virtual ModelMapper CreateModelMapper()
        {
            var mapper = new ConventionModelMapper();

            mapper.BeforeMapClass += (inspector, type, customizer) =>
                                         {
                                             customizer.Table(NamingConvention.Table(inspector, type));

                                             PropertyPath property = inspector.FindPersistentId(type);

                                             if (property != null)
                                             {
                                                 customizer.Id(property.LocalMember, map =>
                                                                                         {
                                                                                             map.Column(
                                                                                                 NamingConvention.Column
                                                                                                     (inspector,
                                                                                                      property));

                                                                                             ApplyIdConventions(map,
                                                                                                                property
                                                                                                                    .
                                                                                                                    LocalMember
                                                                                                                as
                                                                                                                PropertyInfo);
                                                                                         });
                                             }
                                         };

            mapper.BeforeMapJoinedSubclass += (inspector, type, customizer) =>
                                                  {
                                                      var id = inspector.FindPersistentId(type.BaseType);

                                                      
                                                      customizer.Table(NamingConvention.Table(inspector, type));
                                                      customizer.Key(
                                                          key =>
                                                              {
                                                                  key.Column(NamingConvention.KeyColumn(inspector, id,
                                                                                                        type.BaseType));
                                                                  key.ForeignKey(NamingConvention.ForeignKey(inspector, id, type, type.BaseType));
                                                              });
                                                  };

            mapper.BeforeMapProperty += (inspector, member, customizer) =>
                                            {
                                                if (!inspector.IsPersistentId(member.LocalMember) &&
                                                    !inspector.IsPersistentProperty(member.LocalMember))
                                                    return;

                                                customizer.Column(NamingConvention.Column(inspector, member));

                                                Type type = member.LocalMember.GetPropertyOrFieldType();

                                                ApplyPropertyConventions(customizer, member, type, GetMemberAttributes(member.LocalMember));
                                            };

            mapper.BeforeMapManyToOne += (inspector, member, customizer) =>
                                             {
                                                 customizer.Column(NamingConvention.Column(inspector, member));
                                                 customizer.ForeignKey(NamingConvention
                                                                           .ForeignKey(inspector, member));
                                                 customizer.Index(NamingConvention.Index(inspector, member));

                                                 Type type = member.LocalMember.GetPropertyOrFieldType();

                                                 ApplyManyToOneConventions(customizer, member, type, GetMemberAttributes(member.LocalMember));
                                             };

            mapper.BeforeMapBag += (inspector, member, customizer) =>
                                       {
                                           var inverse = GetLikelyInverseProperty(member);

                                           var keyColumn = inverse == null
                                                               ? NamingConvention.KeyColumn(inspector, member)
                                                               : NamingConvention.Column(inspector, inverse);


                                           customizer.Key(key => key.Column(keyColumn));

                                           ApplyBagConventions(customizer, member);
                                       };

            mapper.BeforeMapSet += (inspector, member, customizer) =>
                                       {
                                           var inverse = GetLikelyInverseProperty(member);

                                           var keyColumn = inverse == null
                                                               ? NamingConvention.KeyColumn(inspector, member)
                                                               : NamingConvention.Column(inspector, inverse);

                                           customizer.Key(key => key.Column(keyColumn));

                                           ApplySetConventions(customizer, member);
                                       };

            mapper.BeforeMapList += (inspector, member, customizer) =>
                                        {
                                            var inverse = GetLikelyInverseProperty(member);

                                            var keyColumn = inverse == null
                                                                ? NamingConvention.KeyColumn(inspector, member)
                                                                : NamingConvention.Column(inspector, inverse);

                                            customizer.Key(key => key.Column(keyColumn));
                                            customizer.Index(index => index.Column(NamingConvention.IndexColumn(inspector, member)));

                                            ApplyListConventions(customizer, member);
                                        };

            mapper.BeforeMapMap += (inspector, member, customizer) =>
                                       {
                                           var inverse = GetLikelyInverseProperty(member);

                                           var keyColumn = inverse == null
                                                               ? NamingConvention.KeyColumn(inspector, member)
                                                               : NamingConvention.Column(inspector, inverse);

                                           // TODO: figure out how to set the map key column (not just the foreign key)

                                           customizer.Key(key => key.Column(keyColumn));

                                           ApplyMapConventions(customizer, member);
                                       };


            mapper.IsEntity(IsEntity);
            mapper.IsRootEntity(IsRootEntity);
            //mapper.IsTablePerClassHierarchy(IsTablePerClassHierarchy);

            return mapper;
        }

        protected virtual void ApplyMapConventions(IMapPropertiesMapper mapper, PropertyPath member)
        {
            mapper.Inverse(true);
        }

        protected virtual void ApplyListConventions(IListPropertiesMapper mapper, PropertyPath member)
        {
            mapper.Inverse(true);
        }

        protected virtual void ApplySetConventions(ISetPropertiesMapper mapper, PropertyPath member)
        {
            mapper.Inverse(true);
        }

        /// <summary>
        /// Applies any conventions to bag properties.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="member"> </param>
        protected virtual void ApplyBagConventions(IBagPropertiesMapper mapper, PropertyPath member)
        {
            mapper.Inverse(true);
        }

        /// <summary>
        ///   Applies any conventions required for many-to-one properties.
        /// </summary>
        /// <param name="mapper"> The mapper. </param>
        /// <param name="property"> The property. </param>
        /// <param name="propertyType"> </param>
        /// <param name="attributes"> </param>
        protected virtual void ApplyManyToOneConventions(IManyToOneMapper mapper, PropertyPath property,
                                                         Type propertyType, IEnumerable<Attribute> attributes)
        {
            mapper.NotNullable(!IsNullable(propertyType, attributes));
        }

        /// <summary>
        ///   Applies any conventions required for persistable properties.
        /// </summary>
        /// <param name="attributes"> Attributes present on the property. </param>
        /// <param name="mapper"> The property mapper. </param>
        /// <param name="property"> The property. </param>
        /// <param name="propertyType"> The property type. </param>
        protected virtual void ApplyPropertyConventions(IPropertyMapper mapper, PropertyPath property, Type propertyType,
                                                        IEnumerable<Attribute> attributes)
        {
            mapper.NotNullable(!IsNullable(propertyType, attributes));
        }

        /// <summary>
        ///   Applies any conventions required for the ID property.
        /// </summary>
        /// <param name="map"> The ID mapper. </param>
        /// <param name="property"> The ID property. </param>
        protected virtual void ApplyIdConventions(IIdMapper map, PropertyInfo property)
        {
            if (property.PropertyType == typeof (int) || property.PropertyType == typeof (long))
                map.Generator(Generators.HighLow, g => g.Params(new {max_lo = 100}));
            else if (property.PropertyType == typeof (Guid)) map.Generator(Generators.GuidComb);
        }

        /// <summary>
        /// Returns whether or not a column may be nullable, given a property's type and attributes.
        /// </summary>
        /// <param name="propertyType">Type of the property.</param>
        /// <param name="attributes">Attributes defined for the property.</param>
        /// <returns>True if the column may be nullable; otherwise, false.</returns>
        protected virtual bool IsNullable(Type propertyType, IEnumerable<Attribute> attributes)
        {
            return (propertyType.IsClass ||
                    (propertyType.IsGenericType &&
                     propertyType.GetGenericTypeDefinition() == typeof (Nullable<>))) &&
                   !attributes.Any(t => t is RequiredAttribute);
        }

        /// <summary>
        /// Returns the most likely inverse property for a collection.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        protected virtual PropertyPath GetLikelyInverseProperty(PropertyPath member)
        {

            var declared = member.LocalMember.DeclaringType;
            var collectionType = member.LocalMember.GetPropertyOrFieldType();

            if (!collectionType.IsGenericCollection()) return null;

            var elementType = collectionType.DetermineCollectionElementOrDictionaryValueType();

            var localSimplified = member.LocalMember.Name.Replace(elementType.Name.Pluralize(), string.Empty);

            var otherProperty = elementType
                                    .GetFirstPropertyOfType(declared,
                                                            property =>
                                                                {
                                                                    var otherSimplified =
                                                                        property.Name.Replace(declared.Name,
                                                                                              string.Empty);

                                                                    return otherSimplified.Equals(localSimplified,
                                                                                                  StringComparison.
                                                                                                      InvariantCultureIgnoreCase);
                                                                })
                                ??
                                elementType.GetFirstPropertyOfType(member.LocalMember.DeclaringType);

            return new PropertyPath(null, otherProperty);
        }

        protected virtual IEnumerable<Attribute> GetMemberAttributes(MemberInfo member)
        {
            return member.GetCustomAttributes(true).Cast<Attribute>();
        }

        protected virtual bool IsTablePerClassHierarchy(Type type, bool b)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether or not the type represents a root entity (not a subclass).
        /// </summary>
        /// <param name="type"> </param>
        /// <param name="declared"> </param>
        /// <returns> </returns>
        protected virtual bool IsRootEntity(Type type, bool declared)
        {
            return type.BaseType == EntityBaseType;
        }

        /// <summary>
        ///   Determines whether or not the type is an entity that should be mapped.
        /// </summary>
        /// <param name="type"> </param>
        /// <param name="declared"> </param>
        /// <returns> </returns>
        protected virtual bool IsEntity(Type type, bool declared)
        {
            if (type == EntityBaseType || type.IsInterface
                || type.ImplementsInterfaceDirectly(typeof(IEntityBase))) return false;

            return EntityBaseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Allows manual mappings to be performed.
        /// </summary>
        /// <param name="map">A delegate for the override mapping.</param>
        public void Override(Action<ModelMapper> map)
        {
            Requires.That(map, "map").IsNotNull();

            map(ModelMapper);
        }

        /// <summary>
        ///   Automatically maps any suitable entities in the assembly of the specified type.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        public void AddEntitiesFromAssemblyOf<T>(Func<Type, bool> filter = null)
        {
            IEnumerable<Type> types = typeof (T).Assembly.GetExportedTypes().AsEnumerable();
            if (filter != null) types = types.Where(filter);

            Mappings.Add(ModelMapper.CompileMappingFor(types));
        }

        /// <summary>
        ///   Returns all mappings configured thus far.
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<HbmMapping> Complete()
        {
            return Mappings;
        }
    }
}