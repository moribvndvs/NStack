#region header
// -----------------------------------------------------------------------
//  <copyright file="NumericVariableTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion


using System;

using FluentAssertions;

using NUnit.Framework;

using Moq;

namespace NStack.Conditions
{
    [TestFixture]
    public class NumericVariableTests
    {
        #region Set-up and Tear-down

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
    }
}