﻿#region header
// <copyright file="IUnitOfWorkScope.cs" company="mikegrabski.com">
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

namespace NStack.Data
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