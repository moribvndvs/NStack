#region header

// <copyright file="Guard.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;

using MG.Annotations;

namespace MG
{
    /// <summary>
    /// A utility for enforcing preconditions.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Asserts that the condition is true.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void That([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                string message = null)
        {
            That<ArgumentException>(assertion, message);
        }

        /// <summary>
        /// Asserts that the condition is true.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw if the assertion is false.</typeparam>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        public static void That<TException>([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                            string message = null)
            where TException : Exception
        {
            if (!assertion)
                throw (TException)
                      Activator.CreateInstance(typeof (TException), message ?? "The assertion must result in true.");
        }

        /// <summary>
        /// Asserts that the condition is false.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void Against([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                   string message = null)
        {
            Against<ArgumentException>(assertion, message);
        }

        /// <summary>
        /// Asserts that the condition is false.
        /// </summary>
        /// <typeparam name="TException">The type of exception to throw if the assertion is true.</typeparam>
        /// <param name="assertion">The assertion.</param>
        /// <param name="message">Exception message.</param>
        [AssertionMethod]
        public static void Against<TException>([AssertionCondition(AssertionConditionType.IS_FALSE)] bool assertion,
                                               string message = null)
            where TException : Exception
        {
            if (assertion)
                throw (TException)
                      Activator.CreateInstance(typeof (TException), message ?? "The assertion must result in false.");
        }

        /// <summary>
        /// Asserts that the specified parameter value is not null.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void AgainstNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value,
                                       string parameterName = null, string message = null)
        {
            if (value == null) throw new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Asserts that the specified parameter value is not a null or empty string.
        /// </summary>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public static void AgainstEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value,
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
        public static void InstanceOf<T>(object instance, string message = null)
        {
            if (!(instance is T))
                throw new InvalidOperationException(message ??
                                                    string.Format("Object must be an instance of {0}.",
                                                                  typeof (T).FullName));
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