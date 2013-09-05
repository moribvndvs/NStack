#region header
// <copyright file="IContainerRegistry[T].cs" company="mikegrabski.com">
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

namespace NStack.Configuration
{
    /// <summary>
    /// A contract for <see cref="IContainerRegistry{T}"/> types allows generic interaction with the container.
    /// </summary>
    /// <typeparam name="T">The container type.</typeparam>
    public interface IContainerRegistry<out T> : IContainerRegistry
    {
        /// <summary>
        /// Returns the container.
        /// </summary>
        /// <returns>The container.</returns>
        T Container();
    }
}