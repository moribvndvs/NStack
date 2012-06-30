#region header
// <copyright file="CollectionVariable.cs" company="mikegrabski.com">
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

using System.Collections;
using System.Linq;

using NStack.Annotations;

namespace NStack.Conditions
{
    public abstract class CollectionVariable<T, TThis> : NullableVariable<T, TThis>
        where T : IEnumerable
        where TThis : CollectionVariable<T, TThis>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected CollectionVariable(T value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        /// Asserts that the argument contains no items.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [AssertionMethod]
        public TThis IsEmpty(string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(HasAny(), message ?? "Must be empty.");

            return (TThis) this;
        }

        protected abstract bool HasAny();

        [AssertionMethod]
        public TThis IsNotEmpty(string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(HasAny(), message ?? "Must not be empty.");

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis HasCountOf(int count, string message = null)
        {
            IsNotNull(message);

            int actual = Value.Cast<object>().Count();

            ThrowOnFail(actual == count, message ?? "Must have {0} items (actual: {1}).", count, actual);

            return (TThis) this;
        }

        protected abstract int GetCount();
    }
}