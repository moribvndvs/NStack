#region header
// <copyright file="DelegatedResolver.cs" company="mikegrabski.com">
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

namespace NStack
{
    public class DelegatedResolver : IResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DelegatedResolver(Func<Type, string, object> get, Func<Type, string, bool> isRegistered)
        {
            _get = get;
            _isRegistered = isRegistered;
        }

        private readonly Func<Type, string, object> _get;

        private readonly Func<Type, string, bool> _isRegistered; 

        #region Implementation of IResolver

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public T Get<T>(string name = null)
        {
            return (T)_get(typeof (T), name);
        }

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="type">The type of the service to resolve.</param>
        /// <param name="name">The name of the service.</param>
        /// <returns>The resolved service instance.</returns>
        public object Get(Type type, string name = null)
        {
            return _get(type, name);
        }

        /// <summary>
        /// Returns whether or not the specified service is registered.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="name">The name of the service.</param>
        /// <returns>True if the service has been registered; otherwise, false.</returns>
        public bool IsRegistered<T>(string name = null)
        {
            return _isRegistered(typeof (T), name);
        }

        #endregion
    }
}