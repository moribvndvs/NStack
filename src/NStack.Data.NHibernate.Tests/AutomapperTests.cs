#region header

// <copyright file="AutomapperTests.cs" company="mikegrabski.com">
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

using FluentAssertions;

using NHibernate.Cfg.MappingSchema;
using NHibernate.Type;

using NUnit.Framework;

using NStack.Extensions;

namespace NStack.Data
{
    [TestFixture]
    public class AutoMapperTests
    {
        private HbmMapping _mapping, _guidMapping;

        #region Setup/Teardown

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        #endregion

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            var automapper = new AutoMapper
                                 {
                                     EntityBaseType = typeof (AutoMapperTestEntityBase)
                                 };

            automapper.Override(mapper =>
                                    {
                                        
                                    });
            automapper.MapAssemblyOf<AutoMapperTests>();

            _mapping = automapper.Complete().First();

            automapper = new AutoMapper
                             {
                                 EntityBaseType = typeof(AutoMapperTestEntityBase<Guid>)
                             };
            automapper.MapAssemblyOf<AutoMapperTests>();
            _guidMapping = automapper.Complete().First();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        [Test]
        public void Should_not_add_classes_directly_marked_with_IEntityBase()
        {
            // Act / Assert
            _mapping.RootClasses.Where(c => c.Name == "EntityBase").Should().BeEmpty();

        }

        [Test]
        public void Should_add_root_classes_derived_from_EntityBaseType()
        {
            // Act / Assert
            _mapping.RootClasses.Should().HaveCount(3);
        }

        [Test]
        public void Should_add_joined_classes()
        {
            // Act / Assert
            _mapping.JoinedSubclasses.Should().HaveCount(1);
        }

        [Test]
        public void Should_map_component()
        {
            // Arrange
            HbmComponent component;

            // Act
            component = _mapping.RootClasses.First(c => c.Name == "Parent").Properties.ElementAt(1) as HbmComponent;

            // Assert
            component.Name.Should().Be("Address");

            foreach (var property in component.Properties.Cast<HbmProperty>())
            {
                property.column.Should().Be(property.name.Underscore());
            }
        }

        [Test]
        public void Should_map_Id_as_guid_comb()
        {
            // Arrange
            HbmClass map;
            HbmColumn column;

            // Act
            map = _guidMapping.RootClasses.First(m => m.Name == "ParentWithGuid");
            column = map.Id.Columns.First();

            // Assert
            column.name.Should().Be("id");
            map.Id.generator.@class.Should().Be("guid.comb");
        }
        
        [Test]
        public void Should_map_Id_as_hilo()
        {
            // Arrange
            HbmClass map;
            HbmColumn column;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            column = map.Id.Columns.First();

            // Assert
            column.name.Should().Be("id");
            map.Id.generator.@class.Should().Be("hilo");
        }

        [Test]
        public void Should_map_property_conventions()
        {
            // Arrange
            HbmClass map;
            HbmProperty nullableProperty, notNullableProperty;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            nullableProperty = map.Properties.Where(p => p.Name == "NullableValue").Cast<HbmProperty>().First();
            notNullableProperty = map.Properties.Where(p => p.Name == "NotNullableValue").Cast<HbmProperty>().First();

            // Assert
            nullableProperty.notnull.Should().BeFalse();
            nullableProperty.column.Should().Be("nullable_value");

            notNullableProperty.notnull.Should().BeTrue();
            notNullableProperty.column.Should().Be("not_nullable_value");

        }

        [Test]
        public void Should_map_enum_property_conventions()
        {
            // Arrange
            HbmClass map;
            HbmProperty enumProperty;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            enumProperty = map.Properties.Where(p => p.Name == "TestEnum").Cast<HbmProperty>().First();

            // Assert
            enumProperty.notnull.Should().BeTrue();
            enumProperty.Type.name.Should().Be(typeof (Int32Type).AssemblyQualifiedName); // should be stored as integers rather than strings.

        }

        [Test]
        public void Should_map_manytoone_conventions()
        {
            // Arrange
            HbmClass map;
            HbmManyToOne nullableProperty, notNullableProperty;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            nullableProperty = map.Properties.Where(p => p.Name == "NullableReference").Cast<HbmManyToOne>().First();
            notNullableProperty = map.Properties.Where(p => p.Name == "NotNullableReference").Cast<HbmManyToOne>().First();

            // Assert
            nullableProperty.notnull.Should().BeFalse();
            nullableProperty.column.Should().Be("nullable_reference_id");
            nullableProperty.index.Should().Be("ix_parents_nullable_reference_id");
            nullableProperty.foreignkey.Should().Be("fk_parents_parents_nullable_reference_id");
            
            notNullableProperty.notnull.Should().BeTrue();
            notNullableProperty.column.Should().Be("not_nullable_reference_id");
            notNullableProperty.index.Should().Be("ix_parents_not_nullable_reference_id");
            notNullableProperty.foreignkey.Should().Be("fk_parents_parents_not_nullable_reference_id");
        }

        [Test]
        public void Should_map_bag_conventions()
        {
            // Arrange
            HbmClass map;
            HbmBag property;
            HbmColumn keyColumn;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            property = map.Properties.Where(p => p.Name == "BagChildren").Cast<HbmBag>().First();
            keyColumn = property.key.Columns.First();

            // Assert
            property.inverse.Should().BeTrue();
            keyColumn.name.Should().Be("bag_parent_id");
        }
        
        [Test]
        public void Should_map_set_conventions()
        {
            // Arrange
            HbmClass map;
            HbmSet property;
            HbmColumn keyColumn;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            property = map.Properties.Where(p => p.Name == "SetChildren").Cast<HbmSet>().First();
            keyColumn = property.key.Columns.First();

            // Assert
            property.inverse.Should().BeTrue();
            keyColumn.name.Should().Be("set_parent_id");
        }
        
        [Test]
        public void Should_map_list_conventions()
        {
            // Arrange
            HbmClass map;
            HbmList property;
            HbmColumn keyColumn, indexColumn;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            property = map.Properties.Where(p => p.Name == "ListChildren").Cast<HbmList>().First();
            indexColumn = property.ListIndex.Columns.First();
            keyColumn = property.key.Columns.First();

            // Assert
            property.inverse.Should().BeTrue();
            keyColumn.name.Should().Be("list_parent_id");
            indexColumn.name.Should().Be("list_index");
        }

        [Test, Ignore("Unable to set map key column from ModelMapper.BeginMapMap in current version of NHibernate.")]
        public void Should_map_map_conventions()
        {
            // Arrange
            HbmClass map;
            HbmMap property;
            HbmColumn keyColumn, mapKeyColumn;

            // Act
            map = _mapping.RootClasses.First(m => m.Name == "Parent");
            property = map.Properties.Where(p => p.Name == "DictionaryChildren").Cast<HbmMap>().First();
            keyColumn = property.key.Columns.First();
            mapKeyColumn = (property.Item as HbmMapKey).Columns.First();

            // Assert
            property.inverse.Should().BeTrue();
            keyColumn.name.Should().Be("dictionary_parent_id");
            mapKeyColumn.name.Should().Be("map_key");
        }

        [Test]
        public void Should_map_table_per_class_conventions()
        {
            // Arrange
            HbmJoinedSubclass map;

            // Act
            map = _mapping.JoinedSubclasses.First(c => c.Name == "SeparateTable");

            // Assert
            map.table.Should().Be("separate_tables");
            map.key.Columns.First().name.Should().Be("parent_id");
            map.key.foreignkey.Should().Be("fk_parents_separate_tables_parent_id");
        }

        [Test]
        public void Should_map_table_per_class_hierarchy_conventions()
        {
            // Arrange
            HbmSubclass map;

            // Act
            map = _mapping.SubClasses.First(c => c.Name == "SingleTableA");

            // Assert
            map.discriminatorvalue.Should().Be("a");

        }

        [Test]
        public void Override_mappings_should_take_affect()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmClass root;

            // Act
            autoMapper.EntityBaseType = typeof (AutoMapperTestEntityBase);
            autoMapper.Override(map => map.Class<Parent>(mapper => mapper.Table("PARENT")));
            autoMapper.MapAssemblyOf<AutoMapperTests>(overrideFilter: type => false);

            root = autoMapper.Complete().First().RootClasses.First(c => c.name == "Parent");

            // Assert
            root.table.Should().Be("PARENT");

        }
    }
}