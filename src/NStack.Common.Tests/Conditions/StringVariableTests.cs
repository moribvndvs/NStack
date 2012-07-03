

using System;

using NStack.Extensions;

using NUnit.Framework;

using FluentAssertions;

namespace NStack.Conditions
{
    [TestFixture]
    public class StringVariableTests
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
            
            Ensures.That(empty)
                .IsNullOrEmpty();
            Ensures.That(nullString)
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
            
            s.Invoking(value => Ensures.That(value).IsNullOrEmpty())
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void Contains_should_pass()
        {
            // Arrange
            var s = "This is some value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).Contains("some"))
                .ShouldNotThrow();
            
            s.Invoking(value => Ensures.That(s).Contains("some"))
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
            
            s.Invoking(value => Ensures.That(s).Contains("some"))
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void StartsWith_should_pass()
        {
            // Arrange
            var s = "This is a value";

            // Act / Assert
            s.Invoking(value => Requires.That(s).StartsWith("This"))
                .ShouldNotThrow();
            
            s.Invoking(value => Ensures.That(s).StartsWith("This"))
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
            
            s.Invoking(value => Ensures.That(s).StartsWith("The"))
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void EndsWith_should_pass()
        {
            // Arrange
            var s = "This is a value.";

            // Act / Assert
            s.Invoking(value => Requires.That(s).EndsWith("value."))
                .ShouldNotThrow();
            
            
            s.Invoking(value => Ensures.That(s).EndsWith("value."))
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
            
            
            s.Invoking(value => Ensures.That(s).EndsWith("value"))
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void HasLengthOf_should_pass()
        {
            // Arrange
            var s = "Test";

            // Act / Assert
            s.Invoking(value => Requires.That(s).HasLengthOf(4))
                .ShouldNotThrow();
            
            s.Invoking(value => Ensures.That(s).HasLengthOf(4))
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
            
            s.Invoking(value => Ensures.That(s).HasLengthOf(4))
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void IsBlank_should_pass()
        {
            // Arrange
            var s = "  ";

            // Act / Assert
            s.Invoking(value => Requires.That(s).IsBlank())
                .ShouldNotThrow();
            
            s.Invoking(value => Ensures.That(s).IsBlank())
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
            
            s.Invoking(value => Ensures.That(s).IsBlank())
                .ShouldThrow<PostConditionException>();

        }
    }
}