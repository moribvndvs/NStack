#region header

// <copyright file="EnumerableExtensions.ForEach.cs" company="mikegrabski.com">
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
using System.Collections.Generic;

namespace NStack.Extensions
{
    /// <summary>
    ///     Extensions for enumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Enumerates each item in the collection and invokes the specified action.
        /// </summary>
        /// <typeparam name="T">The type of the element in the collection.</typeparam>
        /// <param name="enumerable">The collection.</param>
        /// <param name="action">The action invoked for each element.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
// ReSharper disable PossibleMultipleEnumeration
            Requires.That(enumerable, () => enumerable)
// ReSharper restore PossibleMultipleEnumeration
                    .IsNotNull();

            foreach (T element in enumerable)
            {
                action(element);
            }
        }

        /// <summary>
        ///     Enumerates each item in the collection and invokes the specified action, which also provides the index of the element.
        /// </summary>
        /// <typeparam name="T">The type of the element in the collection.</typeparam>
        /// <param name="enumerable">The collection.</param>
        /// <param name="action">The action invoked for each element.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
// ReSharper disable PossibleMultipleEnumeration
            Requires.That(enumerable, () => enumerable)
// ReSharper restore PossibleMultipleEnumeration
                    .IsNotNull();

            int c = 0;

            foreach (T element in enumerable)
            {
                action(element, c++);
            }
        }
    }
}