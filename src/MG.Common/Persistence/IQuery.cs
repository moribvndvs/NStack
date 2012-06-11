#region header
// <copyright file="IQuery.cs" company="mikegrabski.com">
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

using System.Collections.Generic;

namespace MG.Persistence
{
    /// <summary>
    /// A contract for <see cref="ICommand"/>s that return an enumeration of entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IQuery<out TEntity> : ICommandWithResult<IEnumerable<TEntity>>
    {
        /// <summary>
        /// Returns the number of records that would be returned by the query.
        /// </summary>
        /// <returns>The number of matching records.</returns>
        int Count();
    }
}