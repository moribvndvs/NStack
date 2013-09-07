#region header

// -----------------------------------------------------------------------
//  <copyright file="SchemaAction.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace NStack.Configuration
{
    /// <summary>
    ///     The action to be performed during schema initialization.
    /// </summary>
    [Flags]
    public enum SchemaAction
    {
        /// <summary>
        ///     No schema initialization will be performed.
        /// </summary>
        Nothing = 0,

        /// <summary>
        /// Schema actions will be scripted.
        /// </summary>
        Script = 1,

        /// <summary>
        /// Schema actions will be executed against the database.
        /// </summary>
        Execute = 2,

        /// <summary>
        /// Schema will be validated.
        /// </summary>
        Verify = 4,

        /// <summary>
        /// Schema will be dropped.
        /// </summary>
        Drop = 8,

        /// <summary>
        /// Schema be created.
        /// </summary>
        Create = 16,

        /// <summary>
        /// Schema will be updated with any missing objects or changes.
        /// </summary>
        Update = 32,

        /// <summary>
        /// Schema will be dropped and recreated.
        /// </summary>
        DropAndCreate = Drop | Create
    }
}