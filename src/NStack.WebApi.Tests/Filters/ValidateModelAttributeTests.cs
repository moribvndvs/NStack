#region header

// <copyright file="ValidateModelAttributeTests.cs" company="mikegrabski.com">
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

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Web.WebApi.Filters
{
    [TestFixture]
    public class ValidateModelAttributeTests
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
        public void Should_set_an_error_response_if_invalid()
        {
            // Arrange
            var attribute = new ValidateModelAttribute();

            var context =
                new HttpActionContext(
                    new HttpControllerContext(new HttpConfiguration(new HttpRouteCollection()),
                                              new HttpRouteData(new HttpRoute()), new HttpRequestMessage()),
                    new ReflectedHttpActionDescriptor());
            context.ModelState.AddModelError("Key1", "Message1");
            context.ModelState.AddModelError("Key2", "Message2");

            // Act
            attribute.OnActionExecuting(context);

            // Assert
            context.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            context.Response.Content.Should().BeOfType<ObjectContent<HttpError>>();
        }
        
        [Test]
        public void Should_not_set_a_response_if_valid()
        {
            // Arrange
            var attribute = new ValidateModelAttribute();

            var context =
                new HttpActionContext(
                    new HttpControllerContext(new HttpConfiguration(new HttpRouteCollection()),
                                              new HttpRouteData(new HttpRoute()), new HttpRequestMessage()),
                    new ReflectedHttpActionDescriptor());

            // Act
            attribute.OnActionExecuting(context);

            // Assert
            context.Response.Should().BeNull();
        }
    }
}