#region header
// <copyright file="InMemoryServiceLocator.cs" company="mikegrabski.com">
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

using System.Linq;

using Microsoft.Practices.ServiceLocation;

namespace NStack.Testing
{
    /// <summary>
    /// An in-memory implementation of <see cref="IServiceLocator"/>.
    /// </summary>
    public class InMemoryServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private readonly Dictionary<string, object> _namedServices = new Dictionary<string, object>();

        /// <summary>
        /// Registers a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="instance">The service instance.</param>
        /// <param name="key">The instance name.</param>
        public void Register<TService>(TService instance, string key = null)
        {
            if (key == null) _services.Add(typeof (TService), instance);
            else _namedServices.Add(key, instance);
        }

        #region Implementation of IServiceProvider

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        public object GetService(Type serviceType)
        {
            if (_services.ContainsKey(serviceType)) return _services[serviceType];

            var instance = _namedServices.FirstOrDefault(p => p.Value.GetType().IsInstanceOfType(serviceType));

            if (instance.Value != null) return instance.Value;

            return null;
        }

        #endregion

        #region Implementation of IServiceLocator

        /// <summary>
        /// Get an instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is an error resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public object GetInstance(Type serviceType)
        {
            if (_services.ContainsKey(serviceType)) return _services[serviceType];

            var instance = _namedServices.FirstOrDefault(p => p.Value.GetType().IsInstanceOfType(serviceType));

            if (instance.Value != null) return instance.Value;

            throw new ActivationException();
        }

        /// <summary>
        /// Get an instance of the given named <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><param name="key">Name the object was registered with.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is an error resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public object GetInstance(Type serviceType, string key)
        {
            if (!_namedServices.ContainsKey(key)) throw new ActivationException();

            return _namedServices[key];
        }

        /// <summary>
        /// Get all instances of the given <paramref name="serviceType"/> currently
        ///             registered in the container.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// A sequence of instances of the requested <paramref name="serviceType"/>.
        /// </returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var results = new List<object>();

            if (_services.ContainsKey(serviceType)) results.Add(_services[serviceType]);

            return results.Union(from pair in _namedServices
                                 where pair.Value.GetType().IsInstanceOfType(serviceType)
                                 select pair.Value);
        }

        /// <summary>
        /// Get an instance of the given <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public TService GetInstance<TService>()
        {
            return (TService)GetInstance(typeof (TService));
        }

        /// <summary>
        /// Get an instance of the given named <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><param name="key">Name the object was registered with.</param><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        public TService GetInstance<TService>(string key)
        {
            return (TService) GetInstance(typeof (TService), key);
        }

        /// <summary>
        /// Get all instances of the given <typeparamref name="TService"/> currently
        ///             registered in the container.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam><exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">if there is are errors resolving
        ///             the service instance.</exception>
        /// <returns>
        /// A sequence of instances of the requested <typeparamref name="TService"/>.
        /// </returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return GetAllInstances(typeof (TService)).Cast<TService>();
        }

        #endregion
    }
}