#region header
// -----------------------------------------------------------------------
//  <copyright file="NHUnitOfWork.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using NHibernate;

namespace NStack.Data
{
    public class NHUnitOfWork : IUnitOfWork
    {
        private ISessionFactory _sessionFactory;

        private bool _disposed;

        private LinkedList<UnitOfWorkScope> _scopes = new LinkedList<UnitOfWorkScope>();

        private Dictionary<UnitOfWorkScope, ISession> _scopeSessions = new Dictionary<UnitOfWorkScope, ISession>();

        public NHUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

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
            if (_disposed) return;

            if (disposing)
            {
                if (_scopes != null && _scopes.Any())
                {
                    foreach (var scope in _scopes)
                    {
                        scope.Committing -= OnScopeCommitting;
                        scope.RollingBack -= OnScopeRollingBack;
                    }

                    _scopes.Clear();
                }

                if (_scopeSessions != null && _scopeSessions.Any())
                {
                    foreach (var session in _scopeSessions.Values)
                    {
                        session.Dispose();
                    }

                    _scopeSessions.Clear();
                }
            }

            _sessionFactory = null;
            _scopes = null;
            _scopeSessions = null;

            _disposed = true;
        }

        #endregion

        internal LinkedList<UnitOfWorkScope> Scopes
        {
            get { return _scopes; }
        } 

        internal Dictionary<UnitOfWorkScope, ISession> ScopeSessions
        {
            get { return _scopeSessions; }
        } 

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Returns an <see cref="IUnitOfWorkScope"/> based on the <paramref name="option"/>.
        /// </summary>
        /// <param name="option">Option for transaction handling.</param>
        /// <param name="isolationLevel">The isolation level for the transaction.</param>
        /// <param name="autoCommit">Whether or not the scope should be automatically committed when it is closed.</param>
        /// <returns>An <see cref="IUnitOfWork"/>.</returns>
        public IUnitOfWorkScope Scope(TransactionOption option = TransactionOption.Required,
                                      IsolationLevel isolationLevel = IsolationLevel.Unspecified,
                                      bool autoCommit = false)
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);

            if (!_scopes.Any()
                || option == TransactionOption.RequiresNew
                || option == TransactionOption.Suppress)
            {
                var scope = new UnitOfWorkScope(option, autoCommit);
                scope.Committing += OnScopeCommitting;
                scope.RollingBack += OnScopeRollingBack;

                var session = _sessionFactory.OpenSession();

                if (option != TransactionOption.Suppress) session.BeginTransaction(isolationLevel);

                _scopes.AddFirst(scope);
                _scopeSessions.Add(scope, session);

                return scope;
            }

            var node = _scopes.First;

            if (node == null) throw new InvalidOperationException("A scope was not created or prematurely removed.");

            return node.Value;
        }

        private void OnScopeRollingBack(UnitOfWorkScope scope)
        {
            scope.Committing -= OnScopeCommitting;
            scope.RollingBack -= OnScopeRollingBack;

            var session = _scopeSessions[scope];

            session.Dispose();

            _scopes.Remove(scope);
            _scopeSessions.Remove(scope);
        }

        private void OnScopeCommitting(UnitOfWorkScope scope)
        {
            scope.Committing -= OnScopeCommitting;
            scope.RollingBack -= OnScopeRollingBack;

            var session = _scopeSessions[scope];
            
            session.Flush();

            if (scope.TransactionOption != TransactionOption.Suppress) session.Transaction.Commit();

            session.Dispose();

            _scopes.Remove(scope);
            _scopeSessions.Remove(scope);
        }

        /// <summary>
        /// Flushes outstanding changes to the persistence store, without committing any pending transactions.
        /// </summary>
        public void Flush()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);

            foreach (var session in _scopeSessions.Values)
            {
                session.Flush();
            }
        }

        #endregion

        /// <summary>
        /// Returns the active <see cref="ISession"/>.
        /// </summary>
        /// <returns>The active <see cref="ISession"/>.</returns>
        public ISession GetSession()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);

            if (!_scopes.Any()) throw new InvalidOperationException("A unit of work scope has not been started yet.");

            var scope = _scopes.First.Value;

            return _scopeSessions[scope];
        }
    }
}