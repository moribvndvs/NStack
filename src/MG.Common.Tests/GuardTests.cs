

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