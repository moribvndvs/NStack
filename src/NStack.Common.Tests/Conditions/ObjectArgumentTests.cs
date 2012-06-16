

using System;

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Conditions
{
    [TestFixture]
    public class ObjectArgumentTests
    {
        private const int Number = 0;

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
        public void IsEqualTo_should_pass()
        {
            // Act / Assert
            Number.Invoking(value => Require.That(value)
                .IsEqualTo(0))
                .ShouldNotThrow();
        }

        [Test]
        public void IsEqualTo_should_throw()
        {
            // Act / Assert
            Number.Invoking(value => Require.That(value)
                .IsEqualTo(1))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void IsNotEqualTo_should_pass()
        {
            // Act / Assert
            Number.Invoking(value => Require.That(value)
                .IsNotEqualTo(1))
                .ShouldNotThrow();
        }

        [Test]
        public void IsNotEqualTo_should_fail()
        {
            // Act / Assert
            Number.Invoking(value => Require.That(value)
                .IsNotEqualTo(0))
                .ShouldThrow<ArgumentException>();
        }
    }
}