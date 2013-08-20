#region header

// <copyright file="DictionaryVariable.cs" company="mikegrabski.com">
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

using System.Collections;

using NStack.Annotations;

namespace NStack.Conditions
{
    public abstract class DictionaryVariable<T, TKey, TValue, TPair, TThis> : CollectionVariable<T, TPair, TThis>
        where T : IEnumerable
        where TThis : DictionaryVariable<T, TKey, TValue, TPair, TThis>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected DictionaryVariable(T value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        [AssertionMethod]
        public TThis ContainsKey(TKey key, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(HasKey(key), message ?? "Must contain the key \"{0}\".", key);

            return (TThis) this;
        }

        protected abstract bool HasKey(TKey key);

        [AssertionMethod]
        public TThis DoesNotContainKey(TKey key, string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(HasKey(key), message ?? "Must not contain key \"{0}\".", key);


            return (TThis) this;
        }

        [AssertionMethod]
        public TThis ContainsValue(TValue value, string message = null)
        {
            IsNotNull(message);

            ThrowOnFail(HasValue(value), message ?? "Must contain value \"{0}\".", value);

            return (TThis) this;
        }

        [AssertionMethod]
        public TThis DoesNotContainValue(TValue value, string message = null)
        {
            IsNotNull(message);

            ThrowOnSuccess(HasValue(value), message ?? "Must not contain value \"{0}\".", value);

            return (TThis) this;
        }

        protected abstract bool HasValue(TValue value);
    }
}