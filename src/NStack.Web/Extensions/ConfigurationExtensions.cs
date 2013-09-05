#region header

// <copyright file="ConfigurationExtensions.cs" company="mikegrabski.com">
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

using System.Configuration;

using NStack.Configuration;

namespace NStack.Extensions
{
    /// <summary>
    ///     Utility extensions for NStack.Configuration.
    /// </summary>
    public static class ConfigurationExtensions
    {
        private const string ConfigTestingKey = "NStack:Testing";

        private const string ConfigDebuggingKey = "NStack:Debugging";

        /// <summary>
        ///     Reads environmental parameters from the app or web.config appKeys section.
        /// </summary>
        /// <typeparam name="T">The configuration type.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The configuration.</returns>
        public static T ReadEnvironmentFromConfig<T>(this T configuration)
            where T : IConfiguration
        {
            configuration.Debugging(ConfigurationManager.AppSettings[ConfigDebuggingKey].ConvertTo<bool>());
            configuration.Debugging(ConfigurationManager.AppSettings[ConfigTestingKey].ConvertTo<bool>());

            return configuration;
        }
    }
}