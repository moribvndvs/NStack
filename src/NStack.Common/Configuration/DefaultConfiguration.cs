#region header

// <copyright file="DefaultConfiguration.cs" company="mikegrabski.com">
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

namespace NStack.Configuration
{
    /// <summary>
    /// The default implementation of <see cref="IConfiguration"/>.
    /// </summary>
    public class DefaultConfiguration : IConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultConfiguration"/>.
        /// </summary>
        /// <param name="containerAdapter">The container adapter.</param>
        public DefaultConfiguration(IContainerAdapter containerAdapter)
        {
            ContainerAdapter = containerAdapter;
        }

        #region Implementation of IConfiguration

        /// <summary>
        ///     Fluently configures an aspect of an application.
        /// </summary>
        /// <typeparam name="T">The aspect type.</typeparam>
        /// <param name="config">The optional delegate that will be invoked to configure details of the aspect.</param>
        /// <returns>The current configuration.</returns>
        public IConfiguration Aspect<T>(Action<T> config = null) where T : IConfigurationAspect
        {
            var aspect = (T) Activator.CreateInstance(typeof (T), this);

            if (config != null) config(aspect);

            aspect.Build();

            return this;
        }

        #endregion

        /// <summary>
        /// Gets or sets the current <see cref="IContainerAdapter"/>.
        /// </summary>
        protected IContainerAdapter ContainerAdapter { get; private set; }
    }
}