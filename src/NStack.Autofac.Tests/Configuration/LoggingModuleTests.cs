#region header
// <copyright file="LoggingModuleTests.cs" company="mikegrabski.com">
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

using Autofac;

using FluentAssertions;

using NStack.Logging;

using NUnit.Framework;

using Moq;

namespace NStack.Configuration
{
    [TestFixture]
    public class LoggingModuleTests
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
        public void Should_create_log_for_activated_type()
        {
            // Arrange
            var logProvider = new Mock<ILogProvider>();
            logProvider.Setup(c => c.Get(It.Is<Type>(type => type == typeof (TestService))))
                       .Returns<Type>(type => Mock.Of<ILog>());

            var builder = new ContainerBuilder();

            builder.RegisterInstance(logProvider.Object)
                   .SingleInstance();
            builder.RegisterType<TestService>()
                   .As<ITestService>()
                   .InstancePerLifetimeScope();

            builder.RegisterModule<LoggingModule>();

            var container = builder.Build();

            // Act
            var service = container.Resolve<ITestService>();

            // Assert
            logProvider.VerifyAll();
        }

        public interface ITestService
        {
            
        }

        public class TestService : ITestService
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public TestService(ILog log)
            {
                _log = log;
            }

            private ILog _log;
        }
    }
}