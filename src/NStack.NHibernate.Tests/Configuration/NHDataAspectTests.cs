#region header
// <copyright file="NHDataAspectTests.cs" company="mikegrabski.com">
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

using NHibernate;
using NHibernate.Dialect;

using NStack.Data;

using NUnit.Framework;

using Moq;

namespace NStack.Configuration
{
    [TestFixture]
    public class NHDataAspectTests
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
        public void EnsurePrepared_should_only_call_Prepare_once()
        {
            // Arrange
            var aspect = new Mock<NHDataAspect>(null);

            // Act
            aspect.Object.EnsurePrepared();
            aspect.Object.EnsurePrepared();

            // Assert
            aspect.Verify(c => c.PrepareConfiguration(), Times.Once());
        }

        [Test]
        public void RegisterOxidations_should_create_and_register_oxidations()
        {
            // Arrange
            var aspect = new NHDataAspect(null);
            var id = new byte[] {0, 1, 2, 3, 4, 5};
            var today = DateTime.Today;

            IOxidation actual = null;

            var container = new Mock<IContainerAdapter>();
            container.Setup(
                c =>
                c.RegisterSingleInstance(typeof (OxidationAdapter.BigInteger), It.IsAny<OxidationAdapter.BigInteger>(),
                                         null))
                   .Callback<Type, object, string>((type, instance, name) => actual = (IOxidation)instance)
                   .Verifiable();

            // Act
            aspect.Oxidation<OxidationAdapter.BigInteger>(c => new object[] {id, today, true});
            aspect.RegisterOxidations(container.Object);

            // Assert
            container.Verify();

            actual.WorkerId.Should().Be(id);
            actual.Epoch.Should().Be(today);

        }

        [Test]
        public void RegisterNHibernate_should_register_services()
        {
            // Arrange
            var aspect = new NHDataAspect(null);
            aspect.Database(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.ConnectionString = "Data Source=:memory:;Version=3;New=True";
                });

            var container = new Mock<IContainerAdapter>();
            
            container.Setup(
                c => c.RegisterSingleInstance<ISessionFactory, ISessionFactory>(It.IsAny<ISessionFactory>(), null))
                     .Verifiable("Did not register single instance ISessionFactory");
            
            container.Setup(
                c => c.Register<ISession, ISession>(It.IsAny<Func<IResolver, ISession>>(), null))
                     .Verifiable("Did not register transient ISession");

            container.Setup(
                c =>
                c.Register<IStatelessSession, IStatelessSession>(It.IsAny<Func<IResolver, IStatelessSession>>(), null))
                     .Verifiable("Did not register transient IStatelessSession");

            container.Setup(c => c.Register<IUnitOfWork, NHUnitOfWork>(null))
                     .Verifiable("Did not register transient IUnitOfWork");

            container.Setup(c => c.RegisterGeneric(typeof(IRepository<>), typeof(NHRepository<>), null))
                .Verifiable("Did not register generic IRepository<>");

            
            container.Setup(c => c.RegisterGeneric(typeof(IRepository<,>), typeof(NHRepository<,>), null))
                .Verifiable("Did not register generic IRepository<,>");


            // Act
            aspect.RegisterNHibernate(container.Object);

            // Assert
            container.VerifyAll();
        }

        [Test]
        public void ExposeConfig_should_invoke_delegate_with_current_NH_config()
        {
            // Arrange
            var aspect = new NHDataAspect(null);
            NHibernate.Cfg.Configuration actual = null;

            // Act
            aspect.ExposeConfig(cfg => actual = cfg);

            // Assert
            actual.Should().Be(aspect.Config);
        }
    }
}