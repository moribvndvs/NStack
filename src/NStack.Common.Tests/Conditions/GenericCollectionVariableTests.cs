

using System;
using System.Collections.Generic;

using NUnit.Framework;

using FluentAssertions;

namespace NStack.Conditions
{
    [TestFixture]
    public class GenericCollectionVariableTests
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
        public void IsEmpty_should_pass()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(list).IsEmpty())
                .ShouldNotThrow();

            list.Invoking(value => Ensures.That(list).IsEmpty())
                .ShouldNotThrow();
        }
        
        [Test]
        public void IsEmpty_should_fail()
        {
            // Arrange
            var list = new List<string> {""};

            // Act / Assert
            list.Invoking(value => Requires.That(list).IsEmpty())
                .ShouldThrow<ArgumentException>();
            
            list.Invoking(value => Ensures.That(list).IsEmpty())
                .ShouldThrow<PostConditionException>();
        } 
        
        [Test]
        public void IsNotEmpty_should_pass()
        {
            // Arrange
            var list = new List<string> {""};

            // Act / Assert
            list.Invoking(value => Requires.That(list).IsNotEmpty())
                .ShouldNotThrow();
            
            list.Invoking(value => Ensures.That(list).IsNotEmpty())
                .ShouldNotThrow();
        }
        
        [Test]
        public void IsNotEmpty_should_fail()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(list).IsNotEmpty())
                .ShouldThrow<ArgumentException>();
            
            list.Invoking(value => Ensures.That(list).IsNotEmpty())
                .ShouldThrow<PostConditionException>();
        }

        [Test]
        public void HasCountOf_should_pass()
        {
            // Arrange
            var list = new List<string> {""};

            // Act / Assert
            list.Invoking(value => Requires.That(list).HasCountOf(1))
                .ShouldNotThrow();
            
            list.Invoking(value => Ensures.That(list).HasCountOf(1))
                .ShouldNotThrow();

        }
        
        [Test]
        public void HasCountOf_should_fail()
        {
            // Arrange
            var list = new List<string> {""};

            // Act / Assert
            list.Invoking(value => Requires.That(list).HasCountOf(0))
                .ShouldThrow<ArgumentException>();
            
            list.Invoking(value => Ensures.That(list).HasCountOf(0))
                .ShouldThrow<PostConditionException>();

        }

        [Test]
        public void DoesNotHaveCountOf_should_pass()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(list).DoesNotHaveCountOf(1))
                .ShouldNotThrow();

            list.Invoking(value => Ensures.That(list).DoesNotHaveCountOf(1))
                .ShouldNotThrow();
        }

        [Test]
        public void DoesNotHaveCountOf_should_fail()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(value).DoesNotHaveCountOf(0))
                .ShouldThrow<ArgumentException>();
            
            list.Invoking(value => Ensures.That(value).DoesNotHaveCountOf(0))
                .ShouldThrow<PostConditionException>();
        }

        [Test]
        public void Contains_should_pass()
        {
            // Arrange
            var list = new List<string> { "Test" };

            // Act / Assert
            list.Invoking(value => Requires.That(list).Contains("Test"))
                .ShouldNotThrow();

            list.Invoking(value => Ensures.That(list).Contains("Test"))
                .ShouldNotThrow();

        }

        [Test]
        public void Contains_should_fail()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(list).Contains("Test"))
                .ShouldThrow<ArgumentException>();

            list.Invoking(value => Ensures.That(list).Contains("Test"))
                .ShouldThrow<PostConditionException>();
        }

        [Test]
        public void DoesNotContain_should_pass()
        {
            // Arrange
            var list = new List<string>();

            // Act / Assert
            list.Invoking(value => Requires.That(list).DoesNotContain("Test"))
                .ShouldNotThrow();

            list.Invoking(value => Ensures.That(list).DoesNotContain("Test"))
                .ShouldNotThrow();

        }
        
        [Test]
        public void DoesNotContain_should_fail()
        {
            // Arrange
            var list = new List<string> {"Test"};

            // Act / Assert
            list.Invoking(value => Requires.That(list).DoesNotContain("Test"))
                .ShouldThrow<ArgumentException>();

            list.Invoking(value => Ensures.That(list).DoesNotContain("Test"))
                .ShouldThrow<PostConditionException>();

        }
    }
}