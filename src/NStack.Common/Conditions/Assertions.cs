#region header
// <copyright file="Assertions.cs" company="mikegrabski.com">
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

namespace NStack.Conditions
{
    /// <summary>
    /// A utility containing assertions.
    /// </summary>
    public static class Assertions
    {
        /// <summary>
        /// Asserts that the condition is true.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void IsTrue([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                string message = null)
        {
            IsTrue<ArgumentException>(assertion, message);
        }

        /// <summary>
        /// Asserts that the condition is true.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw if the assertion is false.</typeparam>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void IsTrue<TException>([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                            string message = null)
            where TException : Exception
        {
            if (!assertion)
                throw (TException)
                      Activator.CreateInstance(typeof(TException), message ?? "The assertion must result in true.");
        }

        /// <summary>
        /// Asserts that the condition is false.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void IsFalse([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                   string message = null)
        {
            IsFalse<ArgumentException>(assertion, message);
        }

        /// <summary>
        /// Asserts that the condition is false.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw if the assertion is true.</typeparam>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void IsFalse<TException>([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                               string message = null)
            where TException : Exception
        {
            if (assertion)
                throw (TException)
                      Activator.CreateInstance(typeof(TException), message ?? "The assertion must result in false.");
        }

        /// <summary>
        /// Asserts that the specified parameter value is not null.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void IsNotNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value,
                                       string parameterName = null, string message = null)
        {
            if (value == null) throw new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Asserts that the specified parameter value is null.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void IsNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value,
                                       string parameterName = null, string message = null)
        {
            if (value != null) throw new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Asserts that the specified parameter value is not a null or empty string.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void IsNotNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
                                        string parameterName = null, string message = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(message ?? "Argument cannot be null or empty", parameterName);
        }

        /// <summary>
        /// Asserts that the object is an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="instance">The instance being asserted.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void IsInstanceOf<T>(object instance, string message = null)
        {
            IsNotNull(instance);

            if (!(instance is T))
                throw new InvalidOperationException(message ??
                                                    string.Format("Object must be an instance of {0}.",
                                                                  typeof(T).FullName));
        }

        /// <summary>
        /// Asserts that the object is not an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">A type.</typeparam>
        /// <param name="instance">The instance being asserted.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void IsNotInstanceOf<T>(object instance, string message = null)
        {
            IsNotNull(instance);

            if ((instance is T))
                throw new InvalidOperationException(message ?? string.Format("Object must not be an instance of {0}",
                                                                             typeof(T).FullName));
        }

        /// <summary>
        /// Asserts that two values are equal.
        /// </summary>
        /// <param name="left">The value on the left.</param>
        /// <param name="right">The value on the right.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void AreEqual(object left, object right, string message = null)
        {
            if (!Equals(left, right)) throw new InvalidOperationException(message ?? "The values must be equivalent.");
        }
    }
}