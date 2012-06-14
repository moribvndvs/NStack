#region header
// <copyright file="EntityTests.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
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

using NUnit.Framework;

using FluentAssertions;

namespace NStack.Models
{
    [TestFixture]
    public class EntityTests
    {
        private class EntityA : Entity<int>
        {
            public EntityA()
            {
            }

            public EntityA(int id)
            {
                Id = id;
            }
        }

        private class EntityB : Entity<int>
        {
            public EntityB()
            {
            }

            public EntityB(int id)
            {
                Id = id;
            }
        }

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
        public void Should_be_same()
        {
            // Arrange
            var actual = new EntityA();

            // Act/Assert
            actual.Should().BeSameAs(actual);
        }

        [Test]
        public void Should_be_equal()
        {
            // Arrange
            var a = new EntityA(1);
            var a2 = new EntityA(1);

            // Act/Assert
            a.Should().Be(a2);
        }

        [Test]
        public void Should_not_be_equal()
        {
            // Arrange
            var a = new EntityA(1);
            var a2 = new EntityA(2);

            var a3 = new EntityA();
            var a4 = new EntityA();

            // Act/Assert
            a.Should().NotBe(a2);
            // transients can never be equal
            a3.Should().NotBe(a4);
        }

        [Test]
        public void Should_not_be_equal_to_another_type()
        {
            // Arrange
            var a = new EntityA(1);
            var b = new EntityB(1);

            // Act/Assert
            a.Should().NotBe(b);
        }

        [Test]
        public void Should_be_transient()
        {
            // Arrange
            var a = new EntityA();

            // Act/Assert
            Assert.That(a.IsTransient(), Is.True);

        }

        [Test]
        public void Should_not_be_transient()
        {
            // Arrange
            var a = new EntityA(1);

            // Act/Assert
            Assert.That(a.IsTransient(), Is.False);

        }

        [Test]
        public void Transients_should_not_be_equal()
        {
            // Arrange
            var a1 = new EntityA();
            var a2 = new EntityA();

            // Act/Assert
            a1.Should().NotBe(a2);

        }
    }
}