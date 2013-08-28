#region header

// <copyright file="UnitOfWorkScope.cs" company="mikegrabski.com">
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

namespace NStack.Data
{
    /// <summary>
    ///     An implementation of <see cref="IUnitOfWorkScope" />.
    /// </summary>
    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        private readonly bool _autoCommit;

        private readonly Guid _id = Guid.NewGuid();

        private bool _committed;

        private bool _disposed;

        private bool _triedCommitting;

        public UnitOfWorkScope(TransactionOption option, bool autoCommit)
        {
            TransactionOption = option;
            _autoCommit = autoCommit;
        }

        public TransactionOption TransactionOption { get; private set; }

        #region IUnitOfWorkScope Members

        /// <summary>
        ///     Gets the ID of the scope.
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }

        /// <summary>
        ///     Commits the changes made in this scope to the underlying transaction.
        /// </summary>
        public void Commit()
        {
            _triedCommitting = true;
            OnCommitting();
            _committed = true;
        }

        #endregion

        public event Action<UnitOfWorkScope> RollingBack;

        public event Action<UnitOfWorkScope> Committing;

        private void OnCommitting()
        {
            if (Committing != null) Committing(this);
        }

        private void OnRollingBack()
        {
            if (RollingBack != null) RollingBack(this);
        }

        #region Implementation of IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
                if (_committed)
                {
                    _disposed = true;
                    return;
                }

                if (!_triedCommitting && _autoCommit) OnCommitting();
                else OnRollingBack();

                _disposed = true;
            }
        }

        #endregion
    }
}