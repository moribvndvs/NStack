#region header
// <copyright file="GuardTests.cs" company="mikegrabski.com">
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

using System;

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace MG
{
    [TestFixture]
    public class GuardTests
    {
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
        public void That_should_throw_exception()
        {
            Assert.Throws<ArgumentException>(() => Guard.That(false));
        }

        [Test]
        public void That_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.That(true));
        }

        [Test]
        public void Against_should_throw_exception()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against(true));

        }

        [Test]
        public void Against_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.Against(false));
        }

        [Test]
        public void AgainstNull_should_throw_exception()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(null));
        }

        [Test]
        public void AgainstNull_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.AgainstNull(string.Empty));
        }

        [Test]
        public void AgainstEmpty_should_throw_exception()
        {
            Assert.Throws<ArgumentException>(() => Guard.AgainstEmpty(string.Empty));
        }

        [Test]
        public void AgainstEmpty_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.AgainstEmpty("test"));
        }

        [Test]
        public void InstanceOf_should_throw_exception()
        {
            Assert.Throws<InvalidOperationException>(() => Guard.InstanceOf<string>(1));
        }

        [Test]
        public void InstanceOf_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.InstanceOf<string>(string.Empty));
        }

        [Test]
        public void AreEqual_should_throw_exception()
        {
            Assert.Throws<InvalidOperationException>(() => Guard.AreEqual("1", "2"));
        }

        [Test]
        public void AreEqual_should_pass()
        {
            Assert.DoesNotThrow(() => Guard.AreEqual(string.Empty, string.Empty));

        }
    }
}