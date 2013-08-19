#region header
// -----------------------------------------------------------------------
//  <copyright file="NumericVariable.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;

namespace NStack.Conditions
{
    public class NumericVariable<T> : Variable<T, NumericVariable<T>>
        where T : struct, IComparable
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NumericVariable(T value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        /// Asserts that the argument is greater than the specified value.
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
        /// Asserts that the argument is greater than or equal to the specified value.
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