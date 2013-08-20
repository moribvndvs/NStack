#region header

// <copyright file="TypeImplementsInterfaceDirectlyTests.cs" company="mikegrabski.com">
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

namespace NStack.Extensions
{
    [TestFixture]
    public class TypeImplementsInterfaceDirectlyTests
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

        private interface IFoo
        {
        }

        private class Base
        {
        }

        private class Directly : IFoo
        {
        }

        private class Indirectly : Directly
        {
        }

        private class Both : Indirectly, IFoo
        {
        }

        [Test]
        public void Should_return_false_if_indirectly_implemented()
        {
            // Arrange
            Type t = typeof (Indirectly);

            // Act
            bool actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Should_return_false_if_not_implemented()
        {
            // Arrange
            Type t = typeof (Base);

            // Act
            bool actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Should_return_true_if_directly_implemented()
        {
            // Arrange
            Type t = typeof (Directly);

            // Act
            bool actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_return_true_if_indirectly_and_directly_implemented()
        {
            // Arrange
            Type t = typeof (Both);

            // Act
            bool actual = t.ImplementsInterfaceDirectly(typeof (IFoo));

            // Assert
            actual.Should().BeTrue();
        }
    }
}