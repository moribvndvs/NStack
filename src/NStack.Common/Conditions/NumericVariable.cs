#region header

// <copyright file="NumericVariable.cs" company="mikegrabski.com">
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

namespace NStack.Conditions
{
    public class NumericVariable<T> : Variable<T, NumericVariable<T>>
        where T : struct, IComparable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NumericVariable(T value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        ///     Asserts that the argument is greater than the specified value.
        /// </summary>
        /// <param name="value">The value to be greater than.</param>
        /// <param name="message">Message displayed if the assertion fails.</param>
        /// <returns></returns>
        public NumericVariable<T> IsGreaterThan(T value, string message = null)
        {
            ThrowOnFail(Value.CompareTo(value) > 0, message ?? "Must be greater than {0}", value);

            return this;
        }

        /// <summary>
        ///     Asserts that the argument is greater than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value to be greater than.</param>
        /// <param name="message">Message displayed if the assertion fails.</param>
        /// <returns></returns>
        public NumericVariable<T> IsGreaterThanOrEqualTo(T value, string message = null)
        {
            ThrowOnFail(Value.CompareTo(value) >= 0, message ?? "Must be greater than or equal to {0}", value);

            return this;
        }
    }
}