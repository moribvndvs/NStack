#region header

// <copyright file="ArgumentBase.cs" company="mikegrabski.com">
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
    /// A base class for asserting argument conditions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TThis"></typeparam>
    public abstract class ArgumentBase<T, TThis>
        where TThis : ArgumentBase<T, TThis>
    {
        /// <summary>
        /// Gets the argument value.
        /// </summary>
        protected T Value { get; private set; }

        /// <summary>
        /// Gets the argument name.
        /// </summary>
        protected string Name { get; private set; }

        /// <summary>
        /// Gets whether or not assertions are post-conditions (if false, assertions are pre-conditions).
        /// </summary>
        protected bool PostCondition { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected ArgumentBase(T value, string name, bool postCondition)
        {
            Value = value;
            Name = name;
            PostCondition = postCondition;
        }

        /// <summary>
        /// Checks that assertion condition is successful, otherwise, throws the appropriate exception.
        /// </summary>
        /// <param name="condition">The condition result.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="args">The exception arguments.</param>
        protected virtual void ThrowOnFail(bool condition, string message, params object[] args)
        {
            if (condition) return;

            Throw(message, args);

        }

        /// <summary>
        /// Checks the assertion condition is unsuccessful, otherwise, throws the appropriate exception.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="args">The exception arguments.</param>
        protected virtual void ThrowOnSuccess(bool condition, string message, params object[] args)
        {
            if (!condition) return;

            Throw(message, args);
        }

        private void Throw(string message, params object[] args)
        {
            message = string.Format(message, args);

            if (PostCondition) throw new PostConditionException(message);
            throw new ArgumentException(message, Name);
        }

        /// <summary>
        /// Asserts the argument value is equal to the specified value.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public TThis IsEqualTo(T other, string message = null)
        {
            if (!Equals(Value, other)) throw new ArgumentException(message ?? "Values must be equal.", Name);

            return (TThis)this;
        }
        
        /// <summary>
        /// Asserts the argument value is not equal to the specified value.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public TThis IsNotEqualTo(T other, string message = null)
        {
            if (Equals(Value, other)) throw new ArgumentException(message ?? "Values must not be equal.", Name);

            return (TThis)this;
        }

        [AssertionMethod]
        public TThis IsMet(bool condition, string message = null)
        {
            if (!condition) throw new ArgumentException(message ?? "A condition was not met.", Name);

            return (TThis) this;
        }

        /// <summary>
        /// Asserts that the object is the same instance as the specified object.
        /// </summary>
        /// <param name="other">The object.</param>
        /// <param name="message">The exception message.</param>
        /// <returns></returns>
        [AssertionMethod]
        public TThis IsSameAs(T other, string message = null)
        {
            ThrowOnFail(ReferenceEquals(Value, other), message ?? "Must be the same instance.");

            return (TThis) this;
        }

        /// <summary>
        /// Asserts that the object is not the same instance as the specified object.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public TThis IsNotSameAs(T other, string message = null)
        {
            ThrowOnSuccess(ReferenceEquals(Value, other), message ?? "Must not be the same instance.");

            return (TThis) this;
        }
    }
}