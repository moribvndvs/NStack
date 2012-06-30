#region header
// <copyright file="StringArgument.cs" company="mikegrabski.com">
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

using NStack.Annotations;

using System.Linq;

namespace NStack.Conditions
{
    public class StringArgument : NullableArgumentBase<string, StringArgument>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public StringArgument(string value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        /// Asserts that the argument is null or empty.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public StringArgument IsNullOrEmpty(string message = null)
        {
            ThrowOnFail(string.IsNullOrEmpty(Value), message ?? "Must be null or empty.");

            return this;
        }

        /// <summary>
        /// Asserts that the argument is not a null or empty string.
        /// </summary>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public StringArgument IsNotNullOrEmpty(string message = null)
        {
            ThrowOnSuccess(string.IsNullOrEmpty(Value), message ?? "Must not be null or empty.");

            return this;
        }

        /// <summary>
        /// Asserts argument contains the specified string, regardless of case.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public StringArgument ContainsEquivalent(string value, string message = null)
        {
            return Contains(value, StringComparison.CurrentCultureIgnoreCase, message);
        }

        /// <summary>
        /// Asserts that the argument contains the specified string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"> </param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument Contains(string value, StringComparison comparisonType = StringComparison.CurrentCulture, string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(Value.IndexOf(value, comparisonType) < 0,
                        message ?? "Must contain the value \"{0}\".", value);

            return this;
        }

        /// <summary>
        /// Asserts that the argument starts with the specified string, regardless of case.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument StartsWithEquivalent(string value, string message = null)
        {
            return StartsWith(value, StringComparison.CurrentCultureIgnoreCase, message);
        }

        /// <summary>
        /// Asserts that the argument starts with the specified string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument StartsWith(string value, StringComparison comparisonType = StringComparison.CurrentCulture, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(Value.StartsWith(value, comparisonType), message ?? "Argument does not begin with \"{0}\"",
                        value);

            return this;
        }

        /// <summary>
        /// Asserts that the argument ends with the specified string, regardless of case.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument EndsWithEquivalent(string value, string message = null)
        {
            return EndsWith(value, StringComparison.CurrentCultureIgnoreCase, message);
        }

        /// <summary>
        /// Asserts that the argument ends with the specfied string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument EndsWith(string value, StringComparison comparisonType = StringComparison.CurrentCulture, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(Value.EndsWith(value, comparisonType), message ?? "Argument does not end with \"{0}\".", value);

           return this;
        }

        /// <summary>
        /// Asserts that the argument has the specified length.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument HasLengthOf(int length, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(Value.Length == length, message ?? "Argument should have a length of {0} (actual: {1}).", length,
                        Value.Length);

            return this;
        }

        private static bool IsBlankString(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            return value.All(Char.IsWhiteSpace);
        }

        /// <summary>
        /// Asserts that the argument is a blank string (not null or empty, containing only white space characters).
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public StringArgument IsBlank(string message = null)
        {
            ThrowOnFail(IsBlankString(Value), message ?? "Argument must be blank (actual value: \"{0}\").", Value);

            return this;
        }
    }
}