#region header

// <copyright file="DefaultNamingConventionTests.cs" company="mikegrabski.com">
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
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Moq;

using NHibernate.Mapping.ByCode;

using NUnit.Framework;

namespace NStack.Data
{
    [TestFixture]
    public class DefaultNamingConventionTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            _inspector.Setup(c => c.IsEntity(It.IsAny<Type>()))
                      .Returns((Type type) => type == typeof (Parent) || type == typeof (ParentWithGuid));

            _inspector.Setup(c => c.IsPersistentId(It.IsAny<MemberInfo>()))
                      .Returns((MemberInfo member) => member.Name == "Id");
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        #endregion

        private readonly DefaultNamingConvention _convention = new DefaultNamingConvention();

        private readonly Mock<IModelInspector> _inspector = new Mock<IModelInspector>();

        private MemberInfo GetMemberInfo<T>(string name)
        {
            return (from member in typeof (T).GetMembers()
                    where member.Name == name
                    select member).FirstOrDefault();
        }

        [Test]
        public void Column_should_conform_to_manytoone_property()
        {
            // Act
            string name = _convention.Column(_inspector.Object,
                                             new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("nullable_reference_id");
        }

        [Test]
        public void Column_should_conform_to_persistent_property()
        {
            // Act
            string name = _convention.Column(_inspector.Object,
                                             new PropertyPath(null, GetMemberInfo<Parent>("FirstName")));

            // Assert
            name.Should().Be("first_name");
        }

        [Test]
        public void Column_should_conform_using_declaringType()
        {
            // Act
            string name = _convention.Column(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("Id")),
                                             typeof (Parent));

            // Assert
            name.Should().Be("parent_id");
        }

        [Test]
        public void ForeignKey_should_conform()
        {
            // Act
            string name = _convention.ForeignKey(_inspector.Object,
                                                 new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("fk_parents_parents_nullable_reference_id");
        }

        [Test]
        public void ForeignKey_should_conform_using_declaring_types()
        {
            // Act
            string name = _convention.ForeignKey(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("Id")),
                                                 typeof (SeparateTable), typeof (Parent));

            // Assert
            name.Should().Be("fk_parents_separate_tables_parent_id");
        }

        [Test]
        public void Index_should_conform()
        {
            // Act
            string name = _convention.Index(_inspector.Object,
                                            new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("ix_parents_nullable_reference_id");
        }

        [Test]
        public void KeyColumn_should_conform()
        {
            // Act
            string name = _convention.KeyColumn(_inspector.Object,
                                                new PropertyPath(null, GetMemberInfo<Parent>("BagChildren")));

            // Assert
            name.Should().Be("parent_id");
        }

        [Test]
        public void KeyColumn_should_conform_when_using_declaringType()
        {
            // Act
            string name = _convention.KeyColumn(_inspector.Object,
                                                new PropertyPath(null, GetMemberInfo<Parent>("Id")), typeof (Parent));

            // Assert
            name.Should().Be("parent_id");
        }

        [Test]
        public void Table_should_conform()
        {
            // Act
            string name = _convention.Table(_inspector.Object, typeof (ParentWithGuid));

            // Assert
            name.Should().Be("parent_with_guids");
        }
    }
}