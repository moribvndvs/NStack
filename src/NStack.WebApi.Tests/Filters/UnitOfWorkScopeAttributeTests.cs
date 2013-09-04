#region header
// <copyright file="UnitOfWorkScopeAttributeTests.cs" company="mikegrabski.com">
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
using System.Data;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using System.Web.Http.Routing;

using FluentAssertions;

using Moq;

using NStack.Data;

using NUnit.Framework;

namespace NStack.WebApi.Filters
{
    [TestFixture]
    public class UnitOfWorkScopeAttributeTests
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
        public void OnActionExecuting_should_create_scope()
        {
            // Arrange
            var scope = new Mock<IUnitOfWorkScope>();
            var filter = new UnitOfWorkScopeAttribute();
            var context = CreateActionExecutedContext(scope).ActionContext;

            // Act
            filter.OnActionExecuting(context);

            // Assert
            context.Request.Properties[UnitOfWorkScopeAttribute.RequestPropertyKey].Should()
                                                                                   .BeAssignableTo<IUnitOfWorkScope>();

        }

        [Test]
        public void OnActionExecuting_should_throw_if_scope_already_exists_for_request()
        {
            // Arrange
            var scope = new Mock<IUnitOfWorkScope>();
            var filter = new UnitOfWorkScopeAttribute();
            var context = CreateActionExecutedContext(scope).ActionContext;

            context.Request.Properties[UnitOfWorkScopeAttribute.RequestPropertyKey] = scope.Object;

            // Act / assert
            filter.Invoking(f => f.OnActionExecuting(context))
                  .ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void OnActionExecuted_should_rollback_scope_on_exception()
        {
            // Arrange
            var scope = new Mock<IUnitOfWorkScope>();
            var filter = new UnitOfWorkScopeAttribute();
            var context = CreateActionExecutedContext(scope, new Exception());

            context.Request.Properties[UnitOfWorkScopeAttribute.RequestPropertyKey] = scope.Object;

            // Act
            filter.OnActionExecuted(context);

            // Assert
            scope.Verify(c => c.Commit(), Times.Never());
            scope.Verify(c => c.Dispose(), Times.Once());

            context.Request.Properties.Should().NotContainKey(UnitOfWorkScopeAttribute.RequestPropertyKey);
        }

        [Test]
        public void OnActionExecuted_should_commit_scope()
        {
            // Arrange
            var scope = new Mock<IUnitOfWorkScope>();
            var filter = new UnitOfWorkScopeAttribute();
            var context = CreateActionExecutedContext(scope);

            context.Request.Properties[UnitOfWorkScopeAttribute.RequestPropertyKey] = scope.Object;

            // Act
            filter.OnActionExecuted(context);

            // Assert
            scope.Verify(c => c.Commit(), Times.Once());
            scope.Verify(c => c.Dispose(), Times.Once());

            context.Request.Properties.Should().NotContainKey(UnitOfWorkScopeAttribute.RequestPropertyKey);
        }

        private static HttpActionExecutedContext CreateActionExecutedContext(Mock<IUnitOfWorkScope> scope, Exception exception = null)
        {
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(c => c.Scope(It.IsAny<TransactionOption>(), It.IsAny<IsolationLevel>(), It.IsAny<bool>()))
               .Returns(scope.Object);

            var di = new Mock<IDependencyResolver>();
            di.Setup(c => c.GetService(typeof (IUnitOfWork)))
              .Returns(uow.Object);


            var context =
                new HttpActionExecutedContext(new HttpActionContext(
                                                  new HttpControllerContext(
                                                      new HttpConfiguration(new HttpRouteCollection())
                                                          {
                                                              DependencyResolver = di.Object
                                                          },
                                                      new HttpRouteData(new HttpRoute()), new HttpRequestMessage()),
                                                  new ReflectedHttpActionDescriptor()), exception);
            return context;
        }
    }
}