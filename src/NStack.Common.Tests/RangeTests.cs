#region header

// <copyright file="RangeTests.cs" company="mikegrabski.com">
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

namespace NStack
{
    [TestFixture]
    public class RangeTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        #endregion

        [Test]
        public void Bounds_cannot_be_null()
        {
            Assert.Throws<ArgumentNullException>(() => new Range<string>(null, null));
        }

        [Test]
        public void Contains_should_return_false()
        {
            // Arrange
            var a = new Range<int>(1, 6);

            // Act
            bool actual = a.Contains(7);

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Contains_should_return_true()
        {
            // Arrange
            var a = new Range<int>(1, 6);

            // Act
            bool actual = a.Contains(4);

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Intersects_should_return_false()
        {
            // Arrange
            var a = new Range<int>(1, 6);
            var b = new Range<int>(6, 10);

            // Act
            bool actual = Range<int>.Intersects(a, b);

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Intersects_should_return_true()
        {
            // Arrange
            var a = new Range<int>(1, 5);
            var b = new Range<int>(4, 10);

            // Act
            bool actual = Range<int>.Intersects(a, b);

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_be_equal()
        {
            // Arrange
            var a = new Range<int>(1, 100);
            var b = new Range<int>(1, 100);

            // Act
            bool actual = a.Equals(b);

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_not_be_equal()
        {
            // Arrange
            var a = new Range<int>(1, 100);
            var b = new Range<int>(1, 99);

            // Act
            bool actual = a.Equals(b);

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Should_throw_if_max_is_less_than_min()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Range<int>(5, 1));
        }
    }
}