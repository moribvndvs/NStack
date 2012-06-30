

using System;

using NUnit.Framework;

using FluentAssertions;

namespace NStack.Conditions
{
    [TestFixture]
    public class ObjectVariableTests
    {
        private static readonly object Obj = 0;

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
            Obj.Invoking(value => Requires.That(value)
                .IsEqualTo(0))
                .ShouldNotThrow();
        }

        [Test]
        public void IsEqualTo_should_throw()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value)
                .IsEqualTo(1))
                .ShouldThrow<ArgumentException>();

        }

        [Test]
        public void IsNotEqualTo_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value)
                .IsNotEqualTo(1))
                .ShouldNotThrow();
        }

        [Test]
        public void IsNotEqualTo_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value)
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

        [Test]
        public void IsNull_should_pass()
        {
            // Arrange
            object o = null;

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNull())
                .ShouldNotThrow();

        }
        
        [Test]
        public void IsNull_should_fail()
        {
            // Arrange
            var o = new object();

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNull())
                .ShouldThrow<ArgumentException>();

        }
        
        [Test]
        public void IsNotNull_should_pass()
        {
            // Arrange
            var o = new object();

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNotNull())
                .ShouldNotThrow();

        }
        
        [Test]
        public void IsNotNull_should_fail()
        {
            // Arrange
            object o = null;

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNotNull())
                .ShouldThrow<ArgumentNullException>();

        }

        [Test]
        public void IsInstanceOf_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsInstanceOf<int>())
                .ShouldNotThrow();

        }
        
        [Test]
        public void IsInstanceOf_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsInstanceOf<string>())
                .ShouldThrow<ArgumentException>();

        }
        
        [Test]
        public void IsNotInstanceOf_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotInstanceOf<string>())
                .ShouldNotThrow();

        }
        
        [Test]
        public void IsNotInstanceOf_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotInstanceOf<int>())
                .ShouldThrow<ArgumentException>();

        }
    }
}