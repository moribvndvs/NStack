#region header

// <copyright file="IFluent.cs" company="mikegrabski.com">
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
using System.ComponentModel;

namespace NStack.Fluent
{
    /// <summary>
    ///   An interface for types that support a fluent syntax.
    /// </summary>
    /// <remarks>
    ///   Unclutters fluent APIs by hiding signatures inherited by <see cref="object" /> in IDEs that recognize <see
    ///    cref="EditorBrowsableAttribute" /> .
    /// </remarks>
    public interface IFluent
    {
        /// <summary>
        ///   Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> .
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> . </returns>
        /// <filterpriority>2</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary>
        ///   Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> .
        /// </summary>
        /// <returns> true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> ; otherwise, false. </returns>
        /// <param name="obj"> The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" /> . </param>
        /// <exception cref="T:System.NullReferenceException">The
        ///   <paramref name="obj" />
        ///   parameter is null.</exception>
        /// <filterpriority>2</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);

        /// <summary>
        ///   Serves as a hash function for a particular type.
        /// </summary>
        /// <returns> A hash code for the current <see cref="T:System.Object" /> . </returns>
        /// <filterpriority>2</filterpriority>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary>
        ///   Gets the <see cref="T:System.Type" /> of the current instance.
        /// </summary>
        /// <returns> The <see cref="T:System.Type" /> instance that represents the exact runtime type of the current instance. </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();
    }
}