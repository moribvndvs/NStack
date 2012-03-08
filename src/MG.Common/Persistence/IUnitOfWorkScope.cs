#region header

// <copyright file="IUnitOfWorkScope.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;

namespace MG.Persistence
{
    /// <summary>
    /// A contract for a class that helps a code block enlist in a unit of work.
    /// </summary>
    public interface IUnitOfWorkScope : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="IUnitOfWork"/> the scope belongs to.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}