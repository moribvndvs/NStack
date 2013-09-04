#region header
// -----------------------------------------------------------------------
//  <copyright file="ConfigurationExtensions.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System.Configuration;

using NStack.Configuration;

namespace NStack.Extensions
{
    /// <summary>
    /// Utility extensions for NStack.Configuration.
    /// </summary>
    public static class ConfigurationExtensions
    {
        private const string ConfigTestingKey = "NStack:Testing";
        private const string ConfigDebuggingKey = "NStack:Debugging";

        /// <summary>
        /// Reads environmental parameters from the app or web.config appKeys section.
        /// </summary>
        /// <typeparam name="T">The configuration type.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The configuration.</returns>
        public static T ReadEnvironmentFromConfig<T>(this T configuration)
            where T : IConfiguration
        {
            configuration.Debugging(ConfigurationManager.AppSettings[ConfigDebuggingKey].ConvertTo<bool>());
            configuration.Debugging(ConfigurationManager.AppSettings[ConfigTestingKey].ConvertTo<bool>());

            return this;
        }
    }
}