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
    public class ObjectArgument<T> : NullableArgumentBase<T, ObjectArgument<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ObjectArgument(T value, string name) : base(value, name)
        {
        }

        /// <summary>
        /// Asserts that the argument is an instance of the specified type.
        /// </summary>
        /// <typeparam name="TType">A type.</typeparam>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public ObjectArgument<T> IsInstanceOf<TType>(string message = null)
        {
            IsNotNull();

            if (!(Value is TType))
                throw new ArgumentException(message ??
                                                    String.Format("Argument must be an instance of {0}.",
                                                                  typeof(T).FullName), Name);

            return this;
        }

        /// <summary>
        /// Asserts that the object is not an instance of the specified type.
        /// </summary>
        /// <typeparam name="TType">A type.</typeparam>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public ObjectArgument<T> IsNotInstanceOf<TType>( string message = null)
        {
            IsNotNull();

            if ((Value is TType))
                throw new ArgumentException(message ?? String.Format("Object must not be an instance of {0}",
                                                                             typeof(T).FullName), Name);

            return this;
        }
    }
}