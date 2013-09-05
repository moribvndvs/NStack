#region header

// <copyright file="LoggingModule.cs" company="mikegrabski.com">
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
using System.Linq;

using Autofac;
using Autofac.Core;

using NStack.Logging;

namespace NStack.Configuration
{
    /// <summary>
    /// A module for configuring special needs for logging.
    /// </summary>
    public class LoggingModule : Module
    {
        /// <summary>
        /// Override to attach module-specific functionality to a
        ///             component registration.
        /// </summary>
        /// <remarks>
        /// This method will be called for all existing <i>and future</i> component
        ///             registrations - ordering is not important.
        /// </remarks>
        /// <param name="componentRegistry">The component registry.</param><param name="registration">The registration to attach functionality to.</param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
                                                              IComponentRegistration registration)
        {
            registration.Preparing += (sender, args) =>
                {
                    Type registeredType = args.Component.Activator.LimitType;

                    args.Parameters =
                        args.Parameters.Union(new[]
                            {
                                new ResolvedParameter((info, context) => info.ParameterType == typeof (ILog),
                                                      (info, context) =>
                                                      context.Resolve<ILogProvider>().Get(registeredType))
                            });
                };
        }
    }
}