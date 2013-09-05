#region header
// <copyright file="NLogLogProvider.cs" company="mikegrabski.com">
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

using NLog;

namespace NStack.Logging
{
    /// <summary>
    /// An implementation of <see cref="ILogProvider"/> that wraps <see cref="LogManager"/>.
    /// </summary>
    public class NLogLogProvider : ILogProvider
    {
        #region Implementation of ILogProvider

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified type.
        /// </summary>
        /// <typeparam name="T">The type the <see cref="ILog"/> will log messages for.</typeparam>
        /// <returns>An <see cref="ILog"/>.</returns>
        public ILog Get<T>()
        {
            return Get(typeof (T));
        }

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified type.
        /// </summary>
        /// <param name="type">The type the <see cref="ILog"/> will log message for.</param>
        /// <returns>An <see cref="ILog"/>.</returns>
        public ILog Get(Type type)
        {
            return Get(type.FullName);
        }

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ILog Get(string key)
        {
            return new NLogLog(LogManager.GetLogger(key));
        }

        #endregion
    }
}