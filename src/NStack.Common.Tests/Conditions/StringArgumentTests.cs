

using System;

using NUnit.Framework;

using FluentAssertions;

namespace NStack.Conditions
{
    [TestFixture]
    public class StringArgumentTests
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
        public void IsNullOrEmpty_should_pass()
        {
            // Arrange
            string empty = string.Empty;
            string nullString = null;

            // Act / Assert
            Requires.That(empty)
                .IsNullOrEmpty();
            Requires.That(nullString)
                .IsNullOrEmpty();
        }

        [Test]
        public void IsNullOrEmpty_should_throw()
        {
            // Arrange
            var s = "test";

            // Act / Assert
            s.Invoking(value => Requires.That(value).IsNullOrEmpty())
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void Contains_should_pass()
        {
            // Arrange
            var s = "This is some value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).Contains("some"))
                .ShouldNotThrow();

        }
        
        [Test]
        public void Contains_should_throw()
        {
            // Arrange
            var s = "This is a value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).Contains("some"))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void StartsWith_should_pass()
        {
            // Arrange
            var s = "This is a value";

            // Act / Assert
            s.Invoking(value => Requires.That(s).StartsWith("This"))
                .ShouldNotThrow();

        }
        
        [Test]
        public void StartsWith_should_fail()
        {
            // Arrange
            var s = "This is a value";

            // Act / Assert
            s.Invoking(value => Requires.That(s).StartsWith("The"))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void EndsWith_should_pass()
        {
            // Arrange
            var s = "This is a value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).EndsWith("value."))
                .ShouldNotThrow();

        }
        
        [Test]
        public void EndsWith_should_fail()
        {
            // Arrange
            var s = "This is a value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).EndsWith("value"))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void HasLengthOf_should_pass()
        {
            // Arrange
            var s = "Test";

            // Act / Assert
            s.Invoking(value => Requires.That(s).HasLengthOf(4))
                .ShouldNotThrow();

        }
        
        [Test]
        public void HasLengthOf_should_fail()
        {
            // Arrange
            var s = "Test.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).HasLengthOf(4))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void IsBlank_should_pass()
        {
            // Arrange
            var s = "  ";

            // Act / Assert
            s.Invoking(value => Requires.That(s).IsBlank())
                .ShouldNotThrow();

        }
        
        [Test]
        public void IsBlank_should_fail()
        {
            // Arrange
            var s = "  a";

            // Act / Assert
            s.Invoking(value => Requires.That(s).IsBlank())
                .ShouldThrow<ArgumentException>();

        }
    }
}