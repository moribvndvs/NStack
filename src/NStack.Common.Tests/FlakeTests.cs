#region header

// <copyright file="FlakeTests.cs" company="mikegrabski.com">
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
    public class FlakeTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void SetUpFixture()
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

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        #endregion

        private const decimal Value = 1645918652519951500771329M;

        private const string EncodedValue = "hx6f92xbkvmyhk4jhp";

        [Test]
        public void CompareTo_should_return_negative_value()
        {
            // Arrange
            var lesser = new Flake(1);
            var greater = new Flake(2);

            // Act
            int actual = lesser.CompareTo(greater);

            // Assert
            actual.Should().BeNegative();
        }

        [Test]
        public void CompareTo_should_return_positive_value()
        {
            // Arrange
            var lesser = new Flake(1);
            var greater = new Flake(2);

            // Act
            int actual = greater.CompareTo(lesser);

            // Assert
            actual.Should().BeGreaterThan(0);
        }

        [Test]
        public void CompareTo_should_return_zero()
        {
            // Arrange
            var greater = new Flake(2);

            // Act
            int actual = greater.CompareTo(greater);

            // Assert
            actual.Should().Be(0);
        }

        [Test]
        public void Equals_should_return_false()
        {
            // Arrange
            var first = new Flake(1);
            var second = new Flake(2);

            // Act
            // Assert

            first.Equals(second).Should().BeFalse();
        }

        [Test]
        public void Equals_should_return_true()
        {
            // Arrange
            var first = new Flake(1);
            var second = new Flake(1);

            // Act
            // Assert

            first.Equals(second).Should().BeTrue();
            first.Equals(first).Should().BeTrue();
        }

        [Test]
        public void IsUnassigned_should_return_false()
        {
            // Arrange
            var value = new Flake(1);

            // Act
            bool actual = value.IsUnassigned();

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void IsUnassigned_should_return_true()
        {
            // Arrange
            var value = new Flake(0);

            // Act
            bool actual = value.IsUnassigned();

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Parse_should_parse_Base24_encoded_string()
        {
            // Arrange
            // Act
            Flake actual = Flake.Parse(EncodedValue);

            // Assert
            actual.Should().Be(new Flake(Value));
        }

        [Test]
        public void Should_assign_from_string()
        {
            // Arrange
            Flake actual = EncodedValue;

            // Act / Assert
            actual.Should().Be(new Flake(Value));
        }

        [Test]
        public void Should_be_greater_than()
        {
            (new Flake(2) > new Flake(1)).Should().BeTrue();
        }

        [Test]
        public void Should_be_greater_than_or_equal_to()
        {
            (new Flake(2) >= new Flake(1)).Should().BeTrue();
            (new Flake(2) >= new Flake(2)).Should().BeTrue();
        }

        [Test]
        public void Should_be_less_than()
        {
            (new Flake(2) < new Flake(3)).Should().BeTrue();
        }

        [Test]
        public void Should_be_less_than_or_equal_to()
        {
            (new Flake(2) <= new Flake(3)).Should().BeTrue();
            (new Flake(2) <= new Flake(2)).Should().BeTrue();
        }

        [Test]
        public void Should_not_accept_negative_values()
        {
            Assert.Throws<ArgumentException>(() => new Flake(-1));
        }

        [Test]
        public void Should_truncate_fractional_values()
        {
            // Arrange
            var actual = new Flake(1.012M);
            var expected = new Flake(1);

            // Act / Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ToString_should_return_base24_string()
        {
            // Arrange
            Flake a = Value;

            string actual = a.ToString();

            actual.Should().Be(EncodedValue);
        }

        [Test]
        public void ToString_should_return_decimal_format()
        {
            // Arrange
            Flake a = Value;

            // Act
            string actual = a.ToString("d");

            // Assert
            actual.Should().Be("1645918652519951500771329");
        }

        [Test]
        public void TryParse_should_not_throw()
        {
            var actual = new Flake();

            actual.Invoking(flake => Flake.TryParse(null, out actual))
                  .ShouldNotThrow();

            actual.Invoking(flake => Flake.TryParse(string.Empty, out actual))
                  .ShouldNotThrow();

            actual.Invoking(flake => Flake.TryParse("21@#", out actual))
                  .ShouldNotThrow();
        }

        [Test]
        public void TryParse_should_parse_from_string()
        {
            // Arrange
            Flake actual;

            // Act
            Flake.TryParse(EncodedValue, out actual).Should().BeTrue();

            // Assert
            actual.Should().Be(new Flake(Value));
        }
    }
}