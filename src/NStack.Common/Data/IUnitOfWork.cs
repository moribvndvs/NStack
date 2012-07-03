#region header

// <copyright file="IUnitOfWork.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
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
using System.Collections.Generic;

namespace NStack.Data
{
    /// <summary>
    ///   A contract for a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///   Gets an enumeration of active <see cref="IUnitOfWorkScope" /> that belong to this unit of work.
        /// </summary>
        IEnumerable<IUnitOfWorkScope> ActiveScopes { get; }
    }
}