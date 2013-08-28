#region header

// <copyright file="IUnitOfWork.cs" company="mikegrabski.com">
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
using System.Data;

namespace NStack.Data
{
    /// <summary>
    ///     A contract for a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Returns an <see cref="IUnitOfWorkScope" /> based on the <paramref name="option" />.
        /// </summary>
        /// <param name="option">Option for transaction handling.</param>
        /// <param name="isolationLevel">The isolation level for the transaction.</param>
        /// <param name="autoCommit">Whether or not the scope should be automatically committed when it is disposed.</param>
        /// <returns>
        ///     An <see cref="IUnitOfWork" />.
        /// </returns>
        IUnitOfWorkScope Scope(TransactionOption option = TransactionOption.Required,
                               IsolationLevel isolationLevel = IsolationLevel.Unspecified, bool autoCommit = false);

        /// <summary>
        ///     Flushes outstanding changes to the persistence store, without committing any pending transactions.
        /// </summary>
        void Flush();
    }
}