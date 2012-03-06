

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace MG.Models
{
    [TestFixture]
    public class EntityTests
    {
        private class EntityA : Entity<int>
        {
            public EntityA()
            {
            }

            public EntityA(int id)
            {
                Id = id;
            }
        }

        private class EntityB : Entity<int>
        {
            public EntityB()
            {
            }

            public EntityB(int id)
            {
                Id = id;
            }
        }

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

        [Test]
        public void Should_be_same()
        {
            // Arrange
            var actual = new EntityA();

            // Act/Assert
            actual.Should().BeSameAs(actual);
        }

        [Test]
        public void Should_be_equal()
        {
            // Arrange
            var a = new EntityA(1);
            var a2 = new EntityA(1);

            // Act/Assert
            a.Should().Be(a2);
        }

        [Test]
        public void Should_not_be_equal()
        {
            // Arrange
            var a = new EntityA(1);
            var a2 = new EntityA(2);

            var a3 = new EntityA();
            var a4 = new EntityA();

            // Act/Assert
            a.Should().NotBe(a2);
            // transients can never be equal
            a3.Should().NotBe(a4);
        }

        [Test]
        public void Should_not_be_equal_to_another_type()
        {
            // Arrange
            var a = new EntityA(1);
            var b = new EntityB(1);

            // Act/Assert
            a.Should().NotBe(b);
        }

        [Test]
        public void Should_be_transient()
        {
            // Arrange
            var a = new EntityA();

            // Act/Assert
            Assert.That(a.IsTransient(), Is.True);

        }

        [Test]
        public void Should_not_be_transient()
        {
            // Arrange
            var a = new EntityA(1);

            // Act/Assert
            Assert.That(a.IsTransient(), Is.False);

        }
    }
}