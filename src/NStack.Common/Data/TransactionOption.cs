#region header

// <copyright file="TransactionOption.cs" company="mikegrabski.com">
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

namespace NStack.Data
{
    /// <summary>
    ///     Indicates how to handle transactions when using a unit of work scope.
    /// </summary>
    public enum TransactionOption
    {
        /// <summary>
        ///     Will join an ambient transaction, otherwise one will be created.
        /// </summary>
        Required,

        /// <summary>
        ///     Always creates a new transaction.
        /// </summary>
        RequiresNew,

        /// <summary>
        ///     Never uses a transaction.
        /// </summary>
        Suppress
    }
}