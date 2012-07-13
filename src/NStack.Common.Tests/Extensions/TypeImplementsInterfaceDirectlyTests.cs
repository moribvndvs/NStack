

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Extensions
{
    [TestFixture]
    public class TypeImplementsInterfaceDirectlyTests
    {
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
        public void Should_return_false_if_not_implemented()
        {
            // Arrange
            var t = typeof (Base);

            // Act
            var actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Should_return_true_if_directly_implemented()
        {
            // Arrange
            var t = typeof (Directly);

            // Act
            var actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_return_false_if_indirectly_implemented()
        {
            // Arrange
            var t = typeof (Indirectly);

            // Act
            var actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeFalse();

        }

        [Test]
        public void Should_return_true_if_indirectly_and_directly_implemented()
        {
            // Arrange
            var t = typeof(Both);

            // Act
            var actual = t.ImplementsInterfaceDirectly(typeof(IFoo));

            // Assert
            actual.Should().BeTrue();
        }

        private interface IFoo
        {
            
        }

        private class Base
        {
            
        }

        private class Directly : IFoo
        {
            
        }

        private class Indirectly : Directly
        {
            
        }

        private class Both : Indirectly, IFoo
        {
            
        }
    }
}