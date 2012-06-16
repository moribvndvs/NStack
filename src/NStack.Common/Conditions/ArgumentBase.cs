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
    public abstract class ArgumentBase<T, TThis>
        where TThis : ArgumentBase<T, TThis>
    {
        protected T Value { get; private set; }

        protected string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected ArgumentBase(T value, string name)
        {
            Value = value;
            Name = name;
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

        public TThis IsMet(bool condition, string message = null)
        {
            if (!condition) throw new ArgumentException(message ?? "A condition was not met.", Name);

            return (TThis) this;
        }
    }
}