#region header

// <copyright file="ObjectVariableTests.cs" company="mikegrabski.com">
//    Copyright 2013 Mike Grabski
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>

#endregion

using System;

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Conditions
{
    [TestFixture]
    public class ObjectVariableTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        #endregion

        private static readonly object Obj = 0;

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
        public void IsInstanceOf_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsInstanceOf<string>())
               .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void IsInstanceOf_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsInstanceOf<int>())
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
        public void IsNotEqualTo_should_pass()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value)
                                          .IsNotEqualTo(1))
               .ShouldNotThrow();
        }

        [Test]
        public void IsNotInstanceOf_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotInstanceOf<int>())
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
        public void IsNotNull_should_fail()
        {
            // Arrange
            object o = null;

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNotNull())
             .ShouldThrow<ArgumentNullException>();
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
        public void IsNotSameAs_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsNotSameAs(Obj))
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
        public void IsNull_should_fail()
        {
            // Arrange
            var o = new object();

            // Act / Assert
            o.Invoking(value => Requires.That(value).IsNull())
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
        public void IsSameAs_should_fail()
        {
            // Act / Assert
            Obj.Invoking(value => Requires.That(value).IsSameAs(new object()))
               .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void IsSameAs_should_pass()
        {
            // act / Assert
            Obj.Invoking(value => Requires.That(value).IsSameAs(Obj))
               .ShouldNotThrow();
        }
    }
}