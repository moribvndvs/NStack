#region header

// <copyright file="BiDictionaryTests.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;

using FluentAssertions;

using NUnit.Framework;

namespace MG.Collections
{
    [TestFixture]
    public class BiDictionaryTests
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

        private static BiDictionary<string, int> CreateDefault()
        {
            return new BiDictionary<string, int> {{"one", 1}};
        }

        [Test]
        public void Add_should_insert_to_left_and_right()
        {
            // Arrange/Act
            BiDictionary<string, int> dictionary = CreateDefault();

            // Assert
            dictionary.Left["one"].Should().Be(1);
            dictionary.Right[1].Should().Be("one");
        }

        [Test]
        public void Add_should_throw_exception_when_same_value_added_to_different_keys()
        {
            // Arrange
            var dictionary = new BiDictionary<string, int>();

            // Act
            dictionary.Add("one", 1);

            // Assert
            dictionary.Invoking(d => d.Add("two", 1))
                .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ContainsKey_and_ContainsValue_should_return_false()
        {
            // Arrange
            BiDictionary<string, int> dictionary = CreateDefault();

            // Act / Assert
            dictionary.ContainsKey("two").Should().BeFalse();
            dictionary.ContainsValue(2).Should().BeFalse();
        }

        [Test]
        public void ContainsKey_and_ContainsValue_should_return_true()
        {
            // Arrange
            BiDictionary<string, int> dictionary = CreateDefault();

            // Act / Assert
            dictionary.ContainsKey("one").Should().BeTrue();
            dictionary.ContainsValue(1).Should().BeTrue();
        }

        [Test]
        public void Left_indexer_should_update_right()
        {
            // Arrange
            BiDictionary<string, int> dictionary = CreateDefault();

            // Act
            dictionary.Left["one"] = 11;

            // Assert
            dictionary.Left.Dictionary.Should().HaveCount(1);
            dictionary.Right.Dictionary.Should().HaveCount(1);

            dictionary.Right[11].Should().Be("one");
        }

        [Test]
        public void Remove_removes_from_left_and_right()
        {
            // Arrange
            BiDictionary<string, int> dictionary = CreateDefault();

            // Act
            dictionary.Remove("one").Should().BeTrue();
            dictionary.Remove("one").Should().BeFalse();

            // Assert
            dictionary.Left.Dictionary.Should().HaveCount(0);
            dictionary.Right.Dictionary.Should().HaveCount(0);
        }

        [Test]
        public void Right_indexer_should_update_left()
        {
            // Arrange
            BiDictionary<string, int> dictionary = CreateDefault();

            // Act
            dictionary.Right[1] = "oonnee";

            // Assert
            dictionary.Right.Dictionary.Should().HaveCount(1);
            dictionary.Left.Dictionary.Should().HaveCount(1);

            dictionary.Left["oonnee"].Should().Be(1);
        }

        [Test]
        public void TryGetKey_returns_true_and_associated_key()
        {
            // Arrange
            BiDictionary<string, int> d = CreateDefault();
            string actual;

            // Act
            d.TryGetKey(1, out actual).Should().BeTrue();

            // Assert
            actual.Should().Be("one");
        }
    }
}