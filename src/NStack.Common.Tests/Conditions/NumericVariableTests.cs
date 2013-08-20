#region header

// <copyright file="NumericVariableTests.cs" company="mikegrabski.com">
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

using NUnit.Framework;

namespace NStack.Conditions
{
    [TestFixture]
    public class NumericVariableTests
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

        [Test]
        public void IsGreaterOrEqualTo_should_fail()
        {
            Assert.Throws<ArgumentException>(() => Requires.That(0).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0M).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0F).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0D).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0U).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0L).IsGreaterThanOrEqualTo(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0UL).IsGreaterThanOrEqualTo(1));

            Assert.Throws<PostConditionException>(() => Ensures.That(0).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0M).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0F).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0D).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0U).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0L).IsGreaterThanOrEqualTo(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0UL).IsGreaterThanOrEqualTo(1));
        }

        [Test]
        public void IsGreaterThanOrEqualTo_should_pass()
        {
            Requires.That(0).IsGreaterThanOrEqualTo(0);
            Requires.That(0M).IsGreaterThanOrEqualTo(0);
            Requires.That(0F).IsGreaterThanOrEqualTo(0);
            Requires.That(0D).IsGreaterThanOrEqualTo(0);
            Requires.That(0U).IsGreaterThanOrEqualTo(0);
            Requires.That(0L).IsGreaterThanOrEqualTo(0);
            Requires.That(0UL).IsGreaterThanOrEqualTo(0);

            Ensures.That(0).IsGreaterThanOrEqualTo(0);
            Ensures.That(0M).IsGreaterThanOrEqualTo(0);
            Ensures.That(0F).IsGreaterThanOrEqualTo(0);
            Ensures.That(0D).IsGreaterThanOrEqualTo(0);
            Ensures.That(0U).IsGreaterThanOrEqualTo(0);
            Ensures.That(0L).IsGreaterThanOrEqualTo(0);
            Ensures.That(0UL).IsGreaterThanOrEqualTo(0);
        }

        [Test]
        public void IsGreaterThan_should_fail()
        {
            Assert.Throws<ArgumentException>(() => Requires.That(0).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0M).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0F).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0D).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0U).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0L).IsGreaterThan(1));
            Assert.Throws<ArgumentException>(() => Requires.That(0UL).IsGreaterThan(1));

            Assert.Throws<PostConditionException>(() => Ensures.That(0).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0M).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0F).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0D).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0U).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0L).IsGreaterThan(1));
            Assert.Throws<PostConditionException>(() => Ensures.That(0UL).IsGreaterThan(1));
        }

        [Test]
        public void IsGreaterThan_should_pass()
        {
            Requires.That(1).IsGreaterThan(0);
            Requires.That(1M).IsGreaterThan(0);
            Requires.That(1F).IsGreaterThan(0);
            Requires.That(1D).IsGreaterThan(0);
            Requires.That(1U).IsGreaterThan(0);
            Requires.That(1L).IsGreaterThan(0);
            Requires.That(1UL).IsGreaterThan(0);

            Ensures.That(1).IsGreaterThan(0);
            Ensures.That(1M).IsGreaterThan(0);
            Ensures.That(1F).IsGreaterThan(0);
            Ensures.That(1D).IsGreaterThan(0);
            Ensures.That(1U).IsGreaterThan(0);
            Ensures.That(1L).IsGreaterThan(0);
            Ensures.That(1UL).IsGreaterThan(0);
        }
    }
}