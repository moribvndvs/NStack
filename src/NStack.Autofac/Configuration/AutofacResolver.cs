#region header
// <copyright file="AutofacResolver.cs" company="mikegrabski.com">
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

namespace NStack.Configuration
{
    /// <summary>
    /// A wrapper for Autofac <see cref="IComponentContext"/> which can be used during component registration in an adapter.
    /// </summary>
    internal class AutofacResolver : IResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AutofacResolver(IComponentContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Autofac <see cref="IComponentContext"/>.
        /// </summary>
        protected IComponentContext Context { get; private set; }

        #region Implementation of IResolver

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public T Get<T>(string name = null)
        {
            if (!string.IsNullOrEmpty(name)) return Context.ResolveNamed<T>(name);

            return Context.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="type">The type of the service to resolve.</param>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public object Get(Type type, string name = null)
        {
            if (!string.IsNullOrEmpty(name)) return Context.ResolveNamed(name, type);

            return Context.Resolve(type);
        }

        /// <summary>
        /// Returns whether or not the specified service is registered.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>True if the service has been registered; otherwise, false.</returns>
        public bool IsRegistered<T>(string name = null)
        {
            if (!string.IsNullOrEmpty(name)) return Context.IsRegisteredWithName<T>(name);

            return Context.IsRegistered<T>();
        }

        #endregion
    }
}