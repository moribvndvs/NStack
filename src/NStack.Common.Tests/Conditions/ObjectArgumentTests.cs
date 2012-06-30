

using System;

using NStack.Extensions;

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Conditions
{
    [TestFixture]
    public class ObjectArgumentTests
    {
        private const int Number = 0;
        private static readonly object Obj = new object();

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
            Number.Invoking(value => Requires.That(value)
                .IsEqualTo(0))
                .ShouldNotThrow();
        }

        [Test]
        public void IsEqualTo_should_throw()
        {
            // Act / Assert
            Number.Invoking(value => Requires.That(value)
                .IsEqualTo(1))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void IsNotEqualTo_should_pass()
        {
            // Act / Assert
            Number.Invoking(value => Requires.That(value)
                .IsNotEqualTo(1))
                .ShouldNotThrow();
        }

        [Test]
        public void IsNotEqualTo_should_fail()
        {
            // Act / Assert
            Number.Invoking(value => Requires.That(value)
                .IsNotEqualTo(0))
                .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void IsSameAs_should_pass()
        {
            // act / Assert
            Obj.Invoking(value => Requires.That(value).IsSameAs(Obj))
                .ShouldNotThrow();
        }

        [Test]
        public void IsSameAs_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsSameAs(new object()))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void IsNotSameAs_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotSameAs(new object()))
                .ShouldNotThrow();
        }
        
        [Test]
        public void IsNotSameAs_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotSameAs(Obj))
                .ShouldThrow<ArgumentException>();
        }
    }
}