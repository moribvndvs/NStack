#region header

// <copyright file="IUnitOfWork.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;
using System.Collections.Generic;

namespace MG.Persistence
{
    /// <summary>
    /// A contract for a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets an enumeration of active <see cref="IUnitOfWorkScope"/> that belong to this unit of work.
        /// </summary>
        IEnumerable<IUnitOfWorkScope> ActiveScopes { get; }
    }
}