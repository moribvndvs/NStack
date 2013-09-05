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
    ///     The default implementation of <see cref="IConfiguration" />.
    /// </summary>
    public class DefaultConfiguration : IConfiguration, IConfigurationEnvironment
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DefaultConfiguration" />.
        /// </summary>
        /// <param name="containerRegistry">The container registry.</param>
        public DefaultConfiguration(IContainerRegistry containerRegistry)
        {
            ContainerRegistry = containerRegistry;
        }

        #region Implementation of IConfiguration

        /// <summary>
        ///     Gets whether or not debugging is enabled.
        /// </summary>
        protected bool DebuggingEnabled { get; private set; }

        /// <summary>
        ///     Gets whether or not testing is enabled.
        /// </summary>
        protected bool TestingEnabled { get; private set; }

        /// <summary>
        ///     Indicates application should be configured for a debug environment.
        /// </summary>
        /// <param name="value">Whether or not debugging should be enabled.</param>
        /// <returns>The current configuration.</returns>
        public IConfiguration Debugging(bool value = true)
        {
            DebuggingEnabled = value;

            return this;
        }

        /// <summary>
        ///     Indicates configuration is being loaded in a testing harness.
        /// </summary>
        /// <param name="value">Whether or not configuration is being loaded in a testing harness.</param>
        /// <returns>The current configuration.</returns>
        public IConfiguration Testing(bool value = true)
        {
            TestingEnabled = value;

            return this;
        }

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

        #region IConfiguration Members

        /// <summary>
        ///     Gets or sets the current <see cref="IContainerRegistry" />.
        /// </summary>
        public IContainerRegistry ContainerRegistry { get; private set; }

        /// <summary>
        ///     Gets the configuration environment.
        /// </summary>
        public IConfigurationEnvironment Environment
        {
            get { return this; }
        }

        #endregion

        #region IConfigurationEnvironment Members

        /// <summary>
        ///     Gets whether or not debugging is enabled.
        /// </summary>
        bool IConfigurationEnvironment.DebuggingEnabled
        {
            get { return DebuggingEnabled; }
        }

        /// <summary>
        ///     Gets whether or not testing is enabled.
        /// </summary>
        bool IConfigurationEnvironment.TestingEnabled
        {
            get { return TestingEnabled; }
        }

        #endregion
    }
}