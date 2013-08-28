#region header
// -----------------------------------------------------------------------
//  <copyright file="InMemoryUnitOfWork.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NStack.Data
{
    internal class InMemoryUnitOfWork : IUnitOfWork
    {
        private List<IUnitOfWorkScope> _scopes = new List<IUnitOfWorkScope>();

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_scopes != null)
                {
                    foreach (var scope in _scopes)
                    {
                        scope.Dispose();
                    }

                    _scopes.Clear();
                }
            }

            _scopes = null;
        }

        #endregion

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Returns an <see cref="IUnitOfWorkScope"/> based on the <paramref name="option"/>.
        /// </summary>
        /// <param name="option">Option for transaction handling.</param>
        /// <param name="isolationLevel">The isolation level for the transaction.</param>
        /// <param name="autoCommit">Whether or not the scope should be automatically committed when it is disposed.</param>
        /// <returns>An <see cref="IUnitOfWork"/>.</returns>
        public IUnitOfWorkScope Scope(TransactionOption option = TransactionOption.Required, IsolationLevel isolationLevel = IsolationLevel.Unspecified,
                                      bool autoCommit = false)
        {
            if (!_scopes.Any() || option == TransactionOption.RequiresNew || option == TransactionOption.Suppress)
            {
                var scope = new UnitOfWorkScope(option, autoCommit);
                _scopes.Insert(0, scope);

                return scope;
            }

            return _scopes.First();
        }

        /// <summary>
        /// Flushes outstanding changes to the persistence store, without committing any pending transactions.
        /// </summary>
        public void Flush()
        {
            // no op
        }

        #endregion
    }
}