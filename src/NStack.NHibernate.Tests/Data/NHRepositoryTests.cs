#region header
// -----------------------------------------------------------------------
//  <copyright file="NhRepositoryTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using NHibernate;

using NUnit.Framework;

using Moq;

namespace NStack.Data
{
    [TestFixture]
    public class NHRepositoryTests
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
        public void Session_should_use_unit_of_work_session()
        {
            // Arrange
            var factory = new Mock<ISessionFactory>();
            factory.Setup(c => c.OpenSession())
                   .Returns(Mock.Of<ISession>());

            var uow = new NHUnitOfWork(factory.Object);

            var repository = new NHRepository<object, int>(uow);

            // Act
            var scope = repository.UnitOfWork.Scope();
            var ignore = repository.Get(1);

            // Assert
            factory.Verify(c => c.OpenSession(), Times.Once());

        }
    }
}