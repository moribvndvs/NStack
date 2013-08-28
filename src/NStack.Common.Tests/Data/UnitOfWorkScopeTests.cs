#region header

// <copyright file="UnitOfWorkScopeTests.cs" company="mikegrabski.com">
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

namespace NStack.Data
{
    [TestFixture]
    public class UnitOfWorkScopeTests
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

        private void AssertEvents(bool autoCommit, Action<UnitOfWorkScope> action, bool expectedRollingBackValue,
                                  bool expectedCommittingValue)
        {
            bool rollingBackCalled = false;
            bool committingCalled = false;
            var scope = new UnitOfWorkScope(TransactionOption.Required, autoCommit);

            scope.Committing += s => committingCalled = true;
            scope.RollingBack += s => rollingBackCalled = true;

            action(scope);

            rollingBackCalled.Should().Be(expectedRollingBackValue);
            committingCalled.Should().Be(expectedCommittingValue);
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

        [Test]
        public void Disposing_with_autocommit_should_raise_Committing_event()
        {
            AssertEvents(true, s => s.Dispose(), false, true);
        }

        [Test]
        public void Disposing_without_Commit_should_raise_RollingBack_event()
        {
            AssertEvents(false, s => s.Dispose(), true, false);
        }
    }
}