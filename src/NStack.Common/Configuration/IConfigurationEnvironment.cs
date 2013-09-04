#region header
// -----------------------------------------------------------------------
//  <copyright file="IConfigurationEnvironment.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion
namespace NStack.Configuration
{
    /// <summary>
    /// A contract for objects that expose properties of the configuration environment.
    /// </summary>
    public interface IConfigurationEnvironment
    {
        /// <summary>
        /// Gets whether or not debugging is enabled.
        /// </summary>
        bool DebuggingEnabled { get; }

        /// <summary>
        /// Gets whether or not testing is enabled.
        /// </summary>
        bool TestingEnabled { get; } 
    }
}