#region header

// <copyright file="UnitOfWorkScopeAttribute.cs" company="mikegrabski.com">
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
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using NStack.Data;

namespace NStack.WebApi.Filters
{
    /// <summary>
    ///     An action filter that creates a <see cref="IUnitOfWorkScope" />.
    /// </summary>
    public class UnitOfWorkScopeAttribute : ActionFilterAttribute
    {
        internal const string RequestPropertyKey = "UnitOfWorkScopeAttribute-Request-Scope";

        private static IUnitOfWorkScope GetScope(IDictionary<string, object> properties, bool throwIfMissing = false)
        {
            if (throwIfMissing && !properties.ContainsKey(RequestPropertyKey))
                throw new InvalidOperationException("Expected IUnitOfWorkScope is missing.");

            if (!properties.ContainsKey(RequestPropertyKey)) return null;

            return properties[RequestPropertyKey] as IUnitOfWorkScope;
        }

        /// <summary>
        ///     Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IUnitOfWorkScope scope = GetScope(actionContext.Request.Properties);

            if (scope != null)
                throw new InvalidOperationException(
                    "An IUnitOfWorkScope has already been started for this request; only one can be active at a time. Additional scopes, however, may be created within an action.");

            var unitOfWork =
                (IUnitOfWork)
                actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof (IUnitOfWork));

            actionContext.Request.Properties[RequestPropertyKey] = unitOfWork.Scope(TransactionOption.Required,
                                                                                    autoCommit: true);
        }

        /// <summary>
        ///     Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IUnitOfWorkScope scope = GetScope(actionExecutedContext.Request.Properties, true);

            try
            {
                if (actionExecutedContext.Exception == null) scope.Commit();
            }
            finally
            {
                actionExecutedContext.Request.Properties.Remove(RequestPropertyKey);

                scope.Dispose();
            }
        }
    }
}