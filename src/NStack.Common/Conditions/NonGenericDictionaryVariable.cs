#region header

// <copyright file="NonGenericDictionaryVariable.cs" company="mikegrabski.com">
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
using System.Collections.Generic;
using System.Linq;

namespace NStack.Conditions
{
    public class NonGenericDictionaryVariable
        : DictionaryVariable<IDictionary, object, object, DictionaryEntry, NonGenericDictionaryVariable>
    {
        private IEnumerable<DictionaryEntry> _linqValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NonGenericDictionaryVariable(IDictionary value, string name, bool postCondition)
            : base(value, name, postCondition)
        {
        }

        protected IEnumerable<DictionaryEntry> LinqValue
        {
            get { return _linqValue ?? (_linqValue = Value.Cast<DictionaryEntry>()); }
        }

        #region Overrides of CollectionVariable<IDictionary,KeyValuePair<object,object>,NonGenericDictionaryVariable>

        /// <summary>
        ///     When implemented, returns whether or not any items are in the collection. If <paramref name="predicate" /> is specified, any items must match it.
        /// </summary>
        /// <param name="predicate"> The optional predicate. </param>
        /// <returns>
        ///     True if the collection contains any items, or at least one item matching the <paramref name="predicate" /> ; otherwise, false.
        /// </returns>
        protected override bool HasAny(Func<DictionaryEntry, bool> predicate = null)
        {
            return predicate == null ? LinqValue.Any() : LinqValue.Any(predicate);
        }

        /// <summary>
        ///     When implemented, returns the total number of items in the collection.
        /// </summary>
        /// <returns> The total number of items in the collection. </returns>
        protected override int GetCount()
        {
            return Value.Count;
        }

        #endregion

        #region Overrides of DictionaryVariable<IDictionary,object,object,NonGenericDictionaryVariable>

        protected override bool HasKey(object key)
        {
            return Value.Contains(key);
        }

        protected override bool HasValue(object value)
        {
            return LinqValue.Any(entry => Equals(entry.Value, value));
        }

        #endregion
    }
}