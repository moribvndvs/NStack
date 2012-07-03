#region header

// <copyright file="Requires.cs" company="mikegrabski.com">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

using NStack.Conditions;
using NStack.Expressions;

namespace NStack
{
    /// <summary>
    ///   A facade for asserting preconditions.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        ///   Begins fluent assertion of preconditions.
        /// </summary>
        /// <param name="value"> The value of the argument. </param>
        /// <param name="argumentName"> The name of the argument. </param>
        /// <returns> </returns>
        public static ObjectVariable That(object value, string argumentName = null)
        {
            return new ObjectVariable(value, argumentName, false);
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions.
        /// </summary>
        /// <param name="value"> THe value of the argument. </param>
        /// <param name="reference"> An expression used to find the argument's name in code. </param>
        /// <returns> </returns>
        public static ObjectVariable That(object value, Expression<Func<object>> reference)
        {
            return That(value, ExpressionUtil.GetFieldOrPropertyName(reference));
        }

        /// <summary>
        ///   Begins fluenet assertion of preconditions on strings.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static StringVariable That(string value, string argumentName = null)
        {
            return new StringVariable(value, argumentName, false);
        }

        /// <summary>
        ///   Begins fluenet assertion of preconditions on strings.
        /// </summary>
        /// <param name="value"> </param>
        /// <param name="reference"> </param>
        /// <returns> </returns>
        public static StringVariable That(string value, Expression<Func<string>> reference)
        {
            return That(value, ExpressionUtil.GetFieldOrPropertyName(reference));
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions on generic dictionaries.
        /// </summary>
        /// <typeparam name="TKey"> </typeparam>
        /// <typeparam name="TValue"> </typeparam>
        /// <param name="dictionary"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static GenericDictionaryVariable<TKey, TValue> That<TKey, TValue>(IDictionary<TKey, TValue> dictionary,
                                                                                 string argumentName = null)
        {
            return new GenericDictionaryVariable<TKey, TValue>(dictionary, argumentName, false);
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions on non-generic dictionaries.
        /// </summary>
        /// <param name="dictionary"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static NonGenericDictionaryVariable That(IDictionary dictionary, string argumentName = null)
        {
            return new NonGenericDictionaryVariable(dictionary, argumentName, false);
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions on generic collections.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="collection"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static GenericCollectionVariable<T> That<T>(IEnumerable<T> collection, string argumentName = null)
        {
            return new GenericCollectionVariable<T>(collection, argumentName, false);
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions on generic collections.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="collection"> </param>
        /// <param name="reference"> </param>
        /// <returns> </returns>
        public static GenericCollectionVariable<T> That<T>(IEnumerable<T> collection,
                                                           Expression<Func<IEnumerable<T>>> reference)
        {
            return That(collection, ExpressionUtil.GetFieldOrPropertyName(reference));
        }

        /// <summary>
        ///   Begins fluent assertion of preconditions on non-generic collections.
        /// </summary>
        /// <param name="collection"> </param>
        /// <param name="argumentName"> </param>
        /// <returns> </returns>
        public static NonGenericCollectionVariable That(IEnumerable collection, string argumentName = null)
        {
            return new NonGenericCollectionVariable(collection, argumentName, false);
        }
    }
}