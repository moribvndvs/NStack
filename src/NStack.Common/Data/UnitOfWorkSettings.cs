#region header
// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkSettings.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion
namespace NStack.Data
{
    /// <summary>
    /// A static type containing unit of work settings.
    /// </summary>
    public static class UnitOfWorkSettings
    {
        /// <summary>
        /// Gets or sets whether or not a unit of work should be committed automatically when it is closed.
        /// </summary>
        public static bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        static UnitOfWorkSettings()
        {
            AutoCommit = true;
        }
    }
}