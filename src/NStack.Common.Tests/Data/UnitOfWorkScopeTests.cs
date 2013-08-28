#region header
// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkScopeTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion


using System;

using FluentAssertions;

using NUnit.Framework;

using Moq;

namespace NStack.Data
{
    [TestFixture]
    public class UnitOfWorkScopeTests
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

        private void AssertEvents(bool autoCommit, Action<UnitOfWorkScope> action, bool expectedRollingBackValue,
                                  bool expectedCommittingValue)
        {
            var rollingBackCalled = false;
            var committingCalled = false;
            var scope = new UnitOfWorkScope(TransactionOption.Required, autoCommit);

            scope.Committing += s => committingCalled = true;
            scope.RollingBack += s => rollingBackCalled = true;

            action(scope);

            rollingBackCalled.Should().Be(expectedRollingBackValue);
            committingCalled.Should().Be(expectedCommittingValue);
        }

        [Test]
        public void Disposing_without_Commit_should_raise_RollingBack_event()
        {
            AssertEvents(false, s => s.Dispose(), true, false);
        }
        
        [Test]
        public void Disposing_with_autocommit_should_raise_Committing_event()
        {
            AssertEvents(true, s => s.Dispose(), false, true);
        }
        
        [Test]
        public void Commit_should_raise_Committing_event()
        {
            
            AssertEvents(false, s =>
                {
                    s.Commit();
                    s.Dispose();
                }, false, true);
        }
    }
}