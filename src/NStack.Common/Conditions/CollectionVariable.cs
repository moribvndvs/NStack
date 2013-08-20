#region header

// <copyright file="CollectionVariable.cs" company="mikegrabski.com">
//    Copyright 2013 Mike Grabski
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
using System.Collections;

using NStack.Annotations;

namespace NStack.Conditions
{
    public abstract class CollectionVariable<T, TItem, TThis> : NullableVariable<T, TThis>
        where T : IEnumerable
        where TThis : CollectionVariable<T, TItem, TThis>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected CollectionVariable(T value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        /// <summary>
        ///     Asserts that the collection contains no items.
        /// </summary>
        /// <param name="message"> </param>
        /// <returns> </returns>
        [AssertionMethod]
        public TThis IsEmpty(string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(HasAny(), message ?? "Must be empty.");

            return (TThis) this;
        }

        /// <summary>
        ///     When implemented, returns whether or not any items are in the collection. If <paramref name="predicate" /> is specified, any items must match it.
        /// </summary>
        /// <param name="predicate"> The optional predicate. </param>
        /// <returns>
        ///     True if the collection contains any items, or at least one item matching the <paramref name="predicate" /> ; otherwise, false.
        /// </returns>
        protected abstract bool HasAny(Func<TItem, bool> predicate = null);

        /// <summary>
        ///     Asserts that the collection is not empty.
        /// </summary>
        /// <param name="message"> </param>
        /// <returns> </returns>
        [AssertionMethod]
        public TThis IsNotEmpty(string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(HasAny(), message ?? "Must not be empty.");

            return (TThis) this;
        }

        /// <summary>
        ///     Asserts that the collection contains the specified number of items.
        /// </summary>
        /// <param name="count"> </param>
        /// <param name="message"> </param>
        /// <returns> </returns>
        [AssertionMethod]
        public TThis HasCountOf(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnFail(actual == count, message ?? "Must have {0} items (actual: {1}).", count, actual);

            return (TThis) this;
        }

        /// <summary>
        ///     When implemented, returns the total number of items in the collection.
        /// </summary>
        /// <returns> The total number of items in the collection. </returns>
        protected abstract int GetCount();

        /// <summary>
        /// </summary>
        /// <param name="item"> </param>
        /// <param name="message"> </param>
        /// <returns> </returns>
        [AssertionMethod]
        public TThis Contains(TItem item, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(HasAny(it => Equals(it, item)), message ?? "Must contain the specified item \"{0}\".", item);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis DoesNotHaveCountOf(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnSuccess(actual == count, message ?? "Must not have {0} items.", count);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis DoesNotContain(TItem item, string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(HasAny(it => Equals(it, item)), message ?? "Must not contain the specified item \"{0}\".",
                           item);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis HasCountLessThanOrEqualTo(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnFail(actual <= count, message ?? "Must contain have {0} item(s) or less (actual: {1}).", count,
                        actual);


            return (TThis) this;
        }

        [AssertionMethod]
        public TThis HasCountGreaterThanOrEqualTo(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnFail(actual >= count, message ?? "Must contain have {0} item(s) or more (actual: {1}).", count,
                        actual);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis HasCountLessThan(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnFail(actual < count, message ?? "Must contain have less than {0} item(s) (actual: {1}).", count,
                        actual);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis HasCountGreaterThan(int count, string message = null)
        {
            IsNotNull(message);

            int actual = GetCount();

            ThrowOnFail(actual > count, message ?? "Must contain have more than {0} item(s) (actual: {1}).", count,
                        actual);

            return (TThis) this;
        }
    }
}