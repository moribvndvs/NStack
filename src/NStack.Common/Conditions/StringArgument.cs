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

namespace NStack.Conditions
{
    public class StringArgument : NullableArgumentBase<string, StringArgument>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public StringArgument(string name, string value) : base(value, name)
        {
        }

        /// <summary>
        /// Asserts that the specified argument is not a null or empty string.
        /// </summary>
        /// <param name="message">The exception message.</param>
        [AssertionMethod]
        public StringArgument IsNotNullOrEmpty(string message = null)
        {
            if (String.IsNullOrEmpty(Value))
                throw new ArgumentException(message ?? "Argument cannot be null or empty", Name);

            return this;
        }
    }
}