#region header

// <copyright file="CollectionVariableTests.cs" company="mikegrabski.com">
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
using System.Collections;

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Conditions
{
    [TestFixture]
    public abstract class CollectionVariableTests<T, TVariable, TItem>
        where TVariable : IEnumerable
        where T : CollectionVariable<TVariable, TItem, T>
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
            InitializeCollections();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        #endregion

        protected T EmptyCollection { get; set; }

        protected T NonEmptyCollection { get; set; }

        protected TItem Item { get; set; }

        protected abstract void InitializeCollections();

        [Test]
        public void Contains_should_fail()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.Contains(Item))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Contains_should_pass()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.Contains(Item))
                              .ShouldNotThrow();
        }

        [Test]
        public void DoesNotContain_should_fail()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.DoesNotContain(Item))
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DoesNotContain_should_pass()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.DoesNotContain(Item))
                           .ShouldNotThrow();
        }

        [Test]
        public void DoesNotHaveCountOf_should_fail()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.DoesNotHaveCountOf(0))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DoesNotHaveCountOf_should_pass()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.DoesNotHaveCountOf(1))
                           .ShouldNotThrow();
        }

        [Test]
        public void HasCountGreaterThanOrEqualTo_should_fail()
        {
            EmptyCollection.Invoking(value => value.HasCountGreaterThanOrEqualTo(int.MaxValue))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void HasCountGreaterThanOrEqualTo_should_pass()
        {
            NonEmptyCollection.Invoking(value => value.HasCountGreaterThanOrEqualTo(1))
                              .ShouldNotThrow();
        }

        [Test]
        public void HasCountGreaterThan_should_fail()
        {
            EmptyCollection.Invoking(value => value.HasCountGreaterThan(1))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void HasCountGreaterThan_should_pass()
        {
            NonEmptyCollection.Invoking(value => value.HasCountGreaterThan(0))
                              .ShouldNotThrow();
        }

        [Test]
        public void HasCountLessThanOrEqualTo_should_fail()
        {
            NonEmptyCollection.Invoking(value => value.HasCountLessThanOrEqualTo(0))
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void HasCountLessThanOrEqualTo_should_pass()
        {
            EmptyCollection.Invoking(value => value.HasCountLessThanOrEqualTo(1))
                           .ShouldNotThrow();
        }

        [Test]
        public void HasCountLessThan_should_fail()
        {
            NonEmptyCollection.Invoking(value => value.HasCountLessThan(1))
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void HasCountLessThan_should_pass()
        {
            EmptyCollection.Invoking(value => value.HasCountLessThan(1))
                           .ShouldNotThrow();
        }

        [Test]
        public void HasCountOf_should_fail()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.HasCountOf(1))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void HasCountOf_should_pass()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.HasCountOf(1))
                              .ShouldNotThrow();
        }

        [Test]
        public void IsEmpty_should_fail()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.IsEmpty())
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void IsEmpty_should_pass()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.IsEmpty())
                           .ShouldNotThrow();
        }

        [Test]
        public void IsNotEmpty_should_fail()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.IsNotEmpty())
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void IsNotEmpty_should_pass()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.IsNotEmpty())
                              .ShouldNotThrow();
        }
    }
}