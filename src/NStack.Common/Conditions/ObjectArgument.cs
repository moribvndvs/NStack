#region header
// <copyright file="ObjectArgument.cs" company="mikegrabski.com">
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
    public class ObjectArgument : NullableArgumentBase<object, ObjectArgument>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ObjectArgument(object value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        /// Asserts that the argument is an instance of the specified type.
        /// </summary>
        /// <typeparam name="TType">A type.</typeparam>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public ObjectArgument IsInstanceOf<TType>(string message = null)
        {
            IsNotNull();

            ThrowOnFail(Value is TType, message ?? "Must be an instance of {0}.", Value.GetType().FullName);

            return this;
        }

        /// <summary>
        /// Asserts that the object is not an instance of the specified type.
        /// </summary>
        /// <typeparam name="TType">A type.</typeparam>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public ObjectArgument IsNotInstanceOf<TType>( string message = null)
        {
            IsNotNull();

            ThrowOnSuccess(Value is TType, message ?? "Must not be an instance of {0}", Value.GetType().FullName);

            return this;
        }
    }
}