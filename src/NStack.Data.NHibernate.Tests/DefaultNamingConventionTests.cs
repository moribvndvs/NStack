
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

        #region Setup/Teardown for fixture

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
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
            var name = _convention.Table(null, typeof (ParentWithGuid));

            // Assert
            name.Should().Be("parent_with_guids");
        }

        [Test]
        public void Column_should_conform_to_persistent_property()
        {
            // Act
            var name = _convention.Column(null, new PropertyPath(null, GetMemberInfo<Parent>("FirstName")));

            // Assert
            name.Should().Be("first_name");

        }
    }
}