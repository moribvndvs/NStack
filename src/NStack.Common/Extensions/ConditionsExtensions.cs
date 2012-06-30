#region header

// <copyright file="ConditionsExtensions.cs" company="mikegrabski.com">
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

using NStack.Conditions;

namespace NStack.Extensions
{
    public static partial class ConditionsExtensions
    {
        /// <summary>
        /// Begins fluent assertion of a precondition.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        public static ObjectArgument Requires(this object value, string argumentName = null)
        {
            return NStack.Requires.That(value, argumentName);
        }

        /// <summary>
        /// Begins fluent assertion of a precondition on a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="argumentName"></param>
        /// <returns></returns>
        public static StringArgument Requires(this string value, string argumentName = null)
        {
            return NStack.Requires.That(value, argumentName);
        }

        public static GenericCollectionArgument<T> Requires<T>(this IEnumerable<T> collection,
                                                               string argumentName = null)
        {
            return NStack.Requires.That(collection, argumentName);
        } 
    }
}