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
using System.Linq.Expressions;

using NStack.Conditions;
using NStack.Expressions;

namespace NStack
{
    /// <summary>
    /// A facade for asserting preconditions.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Begins fluent assertion of preconditions.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <returns></returns>
        public static ObjectArgument<T> That<T>(T value, string argumentName = null)
        {
            return new ObjectArgument<T>(value, argumentName, false);
        }

        /// <summary>
        /// Begins fluent assertion of preconditions.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="value">THe value of the argument.</param>
        /// <param name="reference">An expression used to find the argument's name in code.</param>
        /// <returns></returns>
        public static ObjectArgument<T> That<T>(T value, Expression<Func<T>> reference)
        {
            return That(value, ExpressionUtil.GetFieldOrPropertyName(reference));
        }

        public static StringArgument That(string value, string argumentName = null)
        {
            return new StringArgument(value, argumentName, false);
        }

        public static StringArgument That(string value, Expression<Func<string>> reference)
        {
            return That(value, ExpressionUtil.GetFieldOrPropertyName(reference));
        }
    }
}