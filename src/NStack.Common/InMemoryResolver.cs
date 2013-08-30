#region header
// <copyright file="InMemoryResolver.cs" company="mikegrabski.com">
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

namespace NStack
{
    public class InMemoryResolver : IResolver
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private readonly Dictionary<string, object> _namedServices = new Dictionary<string, object>();

        /// <summary>
        /// Registers a service.
        /// </summary>
        /// <typeparam name="T">The type the service should be registered as.</typeparam>
        /// <param name="instance">The service instance.</param>
        /// <param name="name">The optional name of the service instance.</param>
        public void Register<T>(T instance, string name = null)
        {
            if (name == null) _services.Add(typeof (T), instance);
            else _namedServices.Add(name, instance);
        }


        #region Implementation of IResolver

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public T Get<T>(string name = null)
        {
            if (!string.IsNullOrEmpty(name)) return (T) _namedServices[name];

            return (T) _services[typeof (T)];
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="type">The type of the service to resolve.</param>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public object Get(Type type, string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var instance = _namedServices[name];

                if (!instance.GetType().IsInstanceOfType(type)) throw new ArgumentException("The instance with the specified name is of the specified type.", "name");

                return instance;
            }

            return _services[type];
        }

        /// <summary>
        /// Returns whether or not the specified service is registered.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>True if the service has been registered; otherwise, false.</returns>
        public T IsRegistered<T>(string name = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}