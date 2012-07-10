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

using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using NHibernate.Cfg.MappingSchema;

using NStack.Models;

using NUnit.Framework;

using NStack.Extensions;

namespace NStack.Data
{
    [TestFixture]
    public class AutoMapperTests
    {
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
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        [Test]
        public void Should_add_root_classes_derived_from_EntityBaseType()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            IEnumerable<HbmMapping> compiled;

            // Act
            autoMapper.EntityBaseType = typeof (AutoMapperTestEntityBase);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            compiled = autoMapper.Complete();

            // Assert
            compiled.Should().HaveCount(1);
            compiled.FirstOrDefault().RootClasses.Should().HaveCount(2);
            compiled.FirstOrDefault().JoinedSubclasses.Should().HaveCount(1);
        }

        [Test]
        public void Should_add_root_classes_derived_from_generic_EntityBaseType()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            IEnumerable<HbmMapping> compiled;

            // Act
            autoMapper.EntityBaseType = typeof (AutoMapperTestEntityBase<>);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            compiled = autoMapper.Complete();

            // Assert
            compiled.Should().HaveCount(1);
            compiled.FirstOrDefault().RootClasses.Should().HaveCount(1);
            compiled.FirstOrDefault().JoinedSubclasses.Should().BeEmpty();
        }

        [Test]
        public void Should_map_component()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmComponent component;

            // Act
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>(type => type.Name.Equals("Parent"));
            component = autoMapper.Complete().FirstOrDefault().RootClasses.First().Properties.ElementAt(1) as HbmComponent;

            // Assert
            component.Name.Should().Be("Address");

            foreach (var property in component.Properties.Cast<HbmProperty>())
            {
                property.column.Should().Be(property.name.Underscore());
            }
        }

        [Test]
        public void Should_not_add_abstract_classes()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            IEnumerable<HbmMapping> compiled;

            // Act
            autoMapper.EntityBaseType = typeof (Entity<>);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            compiled = autoMapper.Complete();

            // Assert
            compiled.Should().HaveCount(1);
            compiled.FirstOrDefault().RootClasses.Should().HaveCount(3);
            compiled.FirstOrDefault().JoinedSubclasses.Should().HaveCount(1);
        }

        [Test]
        public void Should_map_Id_as_guid_comb()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmClass map;
            HbmColumn column;

            // Act
            autoMapper.EntityBaseType = typeof(AutoMapperTestEntityBase<>);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            map = autoMapper.Complete().First().RootClasses.First();
            column = map.Id.Columns.First();

            // Assert
            column.name.Should().Be("id");
            map.Id.generator.@class.Should().Be("guid.comb");
        }
        
        [Test]
        public void Should_map_Id_as_hilo()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmClass map;
            HbmColumn column;

            // Act
            autoMapper.EntityBaseType = typeof(AutoMapperTestEntityBase);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            map = autoMapper.Complete().First().RootClasses.First();
            column = map.Id.Columns.First();

            // Assert
            column.name.Should().Be("id");
            map.Id.generator.@class.Should().Be("hilo");
        }

        [Test]
        public void Should_map_property_conventions()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmClass map;
            HbmProperty nullableProperty, notNullableProperty;

            // Act
            autoMapper.EntityBaseType = typeof(AutoMapperTestEntityBase);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            map = autoMapper.Complete().First().RootClasses.First();
            nullableProperty = map.Properties.Where(p => p.Name == "NullableValue").Cast<HbmProperty>().First();
            notNullableProperty = map.Properties.Where(p => p.Name == "NotNullableValue").Cast<HbmProperty>().First();

            // Assert
            nullableProperty.notnull.Should().BeFalse();
            nullableProperty.column.Should().Be("nullable_value");

            notNullableProperty.notnull.Should().BeTrue();
            notNullableProperty.column.Should().Be("not_nullable_value");

        }

        [Test]
        public void Should_map_manytoone_conventions()
        {
            // Arrange
            var autoMapper = new AutoMapper();
            HbmClass map;
            HbmManyToOne nullableProperty, notNullableProperty;

            // Act
            autoMapper.EntityBaseType = typeof(AutoMapperTestEntityBase);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            map = autoMapper.Complete().First().RootClasses.First();
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
            var autoMapper = new AutoMapper();
            HbmClass map;
            HbmBag property;
            HbmColumn keyColumn;

            // Act
            autoMapper.EntityBaseType = typeof(AutoMapperTestEntityBase);
            autoMapper.AddEntitiesFromAssemblyOf<AutoMapperTests>();
            map = autoMapper.Complete().First().RootClasses.First();
            property = map.Properties.Where(p => p.Name == "BagChildren").Cast<HbmBag>().First();
            keyColumn = property.key.Columns.First();

            // Assert
            property.inverse.Should().BeTrue();
            keyColumn.name.Should().Be("bag_parent_id");

        }
    }
}