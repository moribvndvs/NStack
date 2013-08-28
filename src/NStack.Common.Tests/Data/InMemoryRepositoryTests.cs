#region header

// -----------------------------------------------------------------------
//  <copyright file="InMemoryRepositoryTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using NStack.Models;

using NUnit.Framework;

namespace NStack.Data
{
    [TestFixture]
    public class InMemoryRepositoryTests
    {
        private class TestEntity : Entity<int>
        {
            public TestEntity(int id)
            {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
                Id = id;
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            }
        }

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

        [Test]
        public void Get_should_return_item_by_id()
        {
            // Arrange
            var expected = new TestEntity(1);
            var repo = new InMemoryRepository<TestEntity, int>(new[] {expected});


            // Act
            var actual = repo.Get(1);

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Get_should_return_null()
        {
            // Arrange
            var repo = new InMemoryRepository<TestEntity, int>();


            // Act
            var actual = repo.Get(1);

            // Assert
            actual.Should().BeNull();
        }

        [Test]
        public void Add_should_add_to_list()
        {
            // Arrange
            var list = new List<TestEntity>();
            var repo = new InMemoryRepository<TestEntity, int>(list);
            var expected = new TestEntity(1);

            // Act
            repo.Add(expected);

            // Assert
            list.Should().ContainSingle(e => Equals(e, expected));

        }
        
        [Test]
        public void Add_should_not_add_item_if_already_added()
        {
            // Arrange
            var list = new List<TestEntity>(new[] {new TestEntity(1)});
            var repo = new InMemoryRepository<TestEntity, int>(list);
            var expected = new TestEntity(1);

            // Act
            repo.Add(expected);

            // Assert
            list.Should().ContainSingle(e => Equals(e, expected));
        }

        [Test]
        public void Remove_should_remove_item()
        {
            // Arrange
            var list = new List<TestEntity>(new[] {new TestEntity(1)});
            var repo = new InMemoryRepository<TestEntity, int>(list);
            var expected = new TestEntity(1);

            // Act
            repo.Remove(expected);
            
            // Assert
            list.Should().BeEmpty();
        }

        [Test]
        public void Should_provide_query_over_list()
        {
            // Arrange
            var expected = new TestEntity(1);
            var repo = new InMemoryRepository<TestEntity, int>(new List<TestEntity> {expected});

            // Act
            var actual = (from item in repo
                          select item).FirstOrDefault();

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Count_should_consider_all_items()
        {
            // Arrange
            var repo = new InMemoryRepository<TestEntity, int>(new List<TestEntity>
                {
                    new TestEntity(1),
                    new TestEntity(2),
                    new TestEntity(3)
                });

            // Act
            var actual = repo.Count();

            // Assert
            actual.Should().Be(3);

        }
    }
}