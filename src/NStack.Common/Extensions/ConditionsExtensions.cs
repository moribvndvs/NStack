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

using System.Collections;
using System.Collections.Generic;

using NStack.Conditions;

namespace NStack.Extensions
{
    public static class ConditionsExtensions
    {
        #region RequiresThat

        /// <summary>
        ///   Begins fluent assertion of a precondition.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static ObjectVariable RequiresThat(this object value, string argumentName = null)
        {
            return Requires.That(value, argumentName);
        }

        /// <summary>
        ///   Begins fluent assertion of a precondition on a string.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static StringVariable RequiresThat(this string value, string argumentName = null)
        {
            return Requires.That(value, argumentName);
        }

        public static GenericCollectionVariable<T> RequiresThat<T>(this IEnumerable<T> collection,
                                                                   string argumentName = null)
        {
            return Requires.That(collection, argumentName);
        }

        public static NonGenericCollectionVariable RequiresThat(this IEnumerable collection, string argumentName = null)
        {
            return Requires.That(collection, argumentName);
        }

        public static GenericDictionaryVariable<TKey, TValue> RequiresThat<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, string argumentName = null)
        {
            return Requires.That(dictionary, argumentName);
        }

        public static NonGenericDictionaryVariable RequiresThat(IDictionary dictionary, string argumentName = null)
        {
            return Requires.That(dictionary, argumentName);
        }

        #endregion

        #region EnsuresThat

        /// <summary>
        ///   Begins fluent assertion of a post condition.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static ObjectVariable EnsuresThat(this object value, string argumentName = null)
        {
            return Ensures.That(value, argumentName);
        }

        /// <summary>
        ///   Begins fluent assertion of a post condition on a string.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static StringVariable EnsuresThat(this string value, string argumentName = null)
        {
            return Ensures.That(value, argumentName);
        }

        public static GenericCollectionVariable<T> EnsuresThat<T>(this IEnumerable<T> collection,
                                                                  string argumentName = null)
        {
            return Ensures.That(collection, argumentName);
        }

        public static NonGenericCollectionVariable EnsuresThat(this IEnumerable collection, string argumentName = null)
        {
            return Ensures.That(collection, argumentName);
        }

        public static GenericDictionaryVariable<TKey, TValue> EnsuresThat<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, string argumentName = null)
        {
            return Ensures.That(dictionary, argumentName);
        }

        public static NonGenericDictionaryVariable EnsuresThat(IDictionary dictionary, string argumentName = null)
        {
            return Ensures.That(dictionary, argumentName);
        }

        #endregion
    }
}