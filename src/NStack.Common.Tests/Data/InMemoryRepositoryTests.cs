#region header

// <copyright file="InMemoryRepositoryTests.cs" company="mikegrabski.com">
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

        private class TestEntity : Entity<int>
        {
            public TestEntity(int id)
            {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
                Id = id;
// ReSharper restore DoNotCallOverridableMethodsInConstructor
            }
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
            int actual = repo.Count();

            // Assert
            actual.Should().Be(3);
        }

        [Test]
        public void Get_should_return_item_by_id()
        {
            // Arrange
            var expected = new TestEntity(1);
            var repo = new InMemoryRepository<TestEntity, int>(new[] {expected});


            // Act
            TestEntity actual = repo.Get(1);

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Get_should_return_null()
        {
            // Arrange
            var repo = new InMemoryRepository<TestEntity, int>();


            // Act
            TestEntity actual = repo.Get(1);

            // Assert
            actual.Should().BeNull();
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
            TestEntity actual = (from item in repo
                                 select item).FirstOrDefault();

            // Assert
            actual.Should().Be(expected);
        }
    }
}