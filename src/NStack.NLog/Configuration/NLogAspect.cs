#region header
// <copyright file="NLogAspect.cs" company="mikegrabski.com">
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

using NStack.Logging;

namespace NStack.Configuration
{
    public class NLogAspect : ConfigurationAspect, ILoggingAspect
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NLogAspect(IConfiguration configuration) : base(configuration)
        {
        }

        #region Overrides of ConfigurationAspect

        /// <summary>
        /// Where any configuration for the aspect should be done prior to configuring the adapter.
        /// </summary>
        protected override void Configure()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Configures the container with any services resulting from the configuration.
        /// </summary>
        /// <param name="registry"></param>
        protected override void ConfigureContainer(IContainerRegistry registry)
        {
            var provider = new NLogLogProvider();
            LogProvider.Provider = provider;

            registry.RegisterSingleInstance<ILogProvider, NLogLogProvider>(provider);
        }

        #endregion
    }
}