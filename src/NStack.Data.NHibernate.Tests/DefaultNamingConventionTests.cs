
using System;
using System.Linq;
using System.Reflection;

using NHibernate.Mapping.ByCode;

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Data
{
    [TestFixture]
    public class DefaultNamingConventionTests
    {
        private readonly DefaultNamingConvention _convention = new DefaultNamingConvention();
        private readonly Mock<IModelInspector> _inspector = new Mock<IModelInspector>();

        #region Setup/Teardown for fixture

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

        #endregion

        #region Setup/Teardown for each test

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        #endregion

        private MemberInfo GetMemberInfo<T>(string name)
        {
            return (from member in typeof (T).GetMembers()
                    where member.Name == name
                    select member).FirstOrDefault();
        }

        [Test]
        public void Table_should_conform()
        {
            // Act
            var name = _convention.Table(_inspector.Object, typeof (ParentWithGuid));

            // Assert
            name.Should().Be("parent_with_guids");
        }

        [Test]
        public void Column_should_conform_to_persistent_property()
        {
            // Act
            var name = _convention.Column(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("FirstName")));

            // Assert
            name.Should().Be("first_name");
        }
        
        [Test]
        public void Column_should_conform_using_declaringType()
        {
            // Act
            var name = _convention.Column(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("Id")),
                                          typeof (Parent));

            // Assert
            name.Should().Be("parent_id");
        }

        [Test]
        public void Column_should_conform_to_manytoone_property()
        {
            // Act
            var name = _convention.Column(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("nullable_reference_id");

        }

        [Test]
        public void ForeignKey_should_conform()
        {
            // Act
            var name = _convention.ForeignKey(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("fk_parents_parents_nullable_reference_id");

        }
        
        [Test]
        public void ForeignKey_should_conform_using_declaring_types()
        {
            // Act
            var name = _convention.ForeignKey(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("Id")), typeof(SeparateTable), typeof(Parent));

            // Assert
            name.Should().Be("fk_parents_joined_subclassed_parents_parent_id");

        }

        [Test]
        public void Index_should_conform()
        {
            // Act
            var name = _convention.Index(_inspector.Object, new PropertyPath(null, GetMemberInfo<Parent>("NullableReference")));

            // Assert
            name.Should().Be("ix_parents_nullable_reference_id");

        }

        [Test]
        public void KeyColumn_should_conform()
        {
            // Act
            var name = _convention.KeyColumn(_inspector.Object,
                                             new PropertyPath(null, GetMemberInfo<Parent>("BagChildren")));

            // Assert
            name.Should().Be("parent_id");

        }

        [Test]
        public void KeyColumn_should_conform_when_using_declaringType()
        {
            // Act
            var name = _convention.KeyColumn(_inspector.Object,
                                             new PropertyPath(null, GetMemberInfo<Parent>("Id")), typeof(Parent));

            // Assert
            name.Should().Be("parent_id");

        }
    }
}