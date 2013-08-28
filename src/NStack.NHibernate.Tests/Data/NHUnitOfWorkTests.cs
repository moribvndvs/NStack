#region header
// -----------------------------------------------------------------------
//  <copyright file="NHUnitOfWorkTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System.Data;

using FluentAssertions;

using NHibernate;

using NUnit.Framework;

using Moq;

namespace NStack.Data
{
    [TestFixture]
    public class NHUnitOfWorkTests
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

        private static NHUnitOfWork CreateUnitOfWork(out Mock<ISession> session)
        {
            session = new Mock<ISession>();
            session.SetupGet(c => c.Transaction).Returns(Mock.Of<ITransaction>());
            
            var factory = new Mock<ISessionFactory>();
            factory.Setup(c => c.OpenSession())
                   .Returns(session.Object);

            return new NHUnitOfWork(factory.Object);
        }

        [Test]
        public void Scope_should_create_initial_scope()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);

            // Act
            var actual = uow.Scope();

            // Assert
            uow.Scopes.Should().OnlyContain(s => s == actual);
            uow.ScopeSessions.Should().ContainKey(actual as UnitOfWorkScope);

            session.Verify(c => c.BeginTransaction(It.IsAny<IsolationLevel>()), Times.Once());

        }
        
        [Test]
        public void Scope_should_use_existing_scope()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var expected = uow.Scope();

            // Act
            var actual = uow.Scope();

            // Assert
            uow.Scopes.Should().OnlyContain(s => s == actual);
            uow.ScopeSessions.Should().HaveCount(1);
            uow.ScopeSessions.Should().ContainKey(actual as UnitOfWorkScope);

            session.Verify(c => c.BeginTransaction(It.IsAny<IsolationLevel>()), Times.Once());
        }

        [Test]
        public void Scope_should_create_new_scope()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var expected = uow.Scope();

            // Act
            var actual = uow.Scope(TransactionOption.RequiresNew);

            // Assert
            uow.Scopes.Should().ContainInOrder(actual, expected);
            uow.ScopeSessions.Should().HaveCount(2);
            uow.ScopeSessions.Should().ContainKey(actual as UnitOfWorkScope);
            uow.ScopeSessions.Should().ContainKey(expected as UnitOfWorkScope);

            session.Verify(c => c.BeginTransaction(It.IsAny<IsolationLevel>()), Times.Exactly(2));
        }

        [Test]
        public void Scope_should_create_new_scope_without_transaction()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var expected = uow.Scope();

            // Act
            var actual = uow.Scope(TransactionOption.Suppress);

            // Assert
            uow.Scopes.Should().ContainInOrder(actual, expected);
            uow.ScopeSessions.Should().HaveCount(2);
            uow.ScopeSessions.Should().ContainKey(actual as UnitOfWorkScope);
            uow.ScopeSessions.Should().ContainKey(expected as UnitOfWorkScope);

            session.Verify(c => c.BeginTransaction(It.IsAny<IsolationLevel>()), Times.Once());
        }

        [Test]
        public void Committing_scope_should_commit_session_and_dispose_it()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);

            // Act
            using (var scope = uow.Scope())
            {
                scope.Commit();
            }

            // Assert
            uow.Scopes.Should().HaveCount(0);
            uow.ScopeSessions.Should().HaveCount(0);

            session.Verify(c => c.Flush(), Times.Once());
            session.Verify(c => c.Dispose(), Times.Once());
        }

        [Test]
        public void RollingBack_scope_should_not_commit_and_dispose_it()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);

            // Act
            using (var scope = uow.Scope())
            {
            }

            // Assert
            uow.Scopes.Should().HaveCount(0);
            uow.ScopeSessions.Should().HaveCount(0);

            session.Verify(c => c.Flush(), Times.Never());
            session.Verify(c => c.Dispose(), Times.Once());
        }

        [Test]
        public void Dispose_should_dispose_remaining_sessions_and_remove_remaining_scopes()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var scope1 = uow.Scope();
            var scope2 = uow.Scope(TransactionOption.RequiresNew);

            // Act
            uow.Dispose();

            // Assert
            uow.ScopeSessions.Should().BeNull();
            uow.Scopes.Should().BeNull();

            session.Verify(c => c.Flush(), Times.Never());
            session.Verify(c => c.Dispose(), Times.Exactly(2));
        }

        [Test]
        public void Flush_should_flush_all_sessions()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var scope = uow.Scope();
            var scope2 = uow.Scope(TransactionOption.RequiresNew);

            // Act
            uow.Flush();

            // Assert
            session.Verify(c => c.Flush(), Times.Exactly(2));
        }

        [Test]
        public void GetSession_should_return_most_recent_session()
        {
            // Arrange
            Mock<ISession> session;
            var uow = CreateUnitOfWork(out session);
            var scope = uow.Scope();
            var scope2 = uow.Scope(TransactionOption.RequiresNew);

            // Act
            var actual = uow.GetSession();

            // Assert
            actual.Should().Be(uow.ScopeSessions[scope2 as UnitOfWorkScope]);
        }
    }
}