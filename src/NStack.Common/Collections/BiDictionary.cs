#region header
// <copyright file="BiDictionary.cs" company="mikegrabski.com">
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
using System.Collections;
using System.Collections.Generic;

namespace NStack.Collections
{
    /// <summary>
    ///   A bidirectional dictionary, allowing a value to be looked up from a key, and a key from a value.
    /// </summary>
    /// <remarks>
    ///   When used as a normal dictionary (as <see cref="IDictionary{TKey,TValue}" /> ), the key type is <typeparamref
    ///    name="TLeft" /> and the value type is <typeparamref name="TRight" />
    /// </remarks>
    /// <typeparam name="TLeft"> The type of the key side of the bidirectional dictionary. </typeparam>
    /// <typeparam name="TRight"> The type of the value side of the bidirectional dictionary. </typeparam>
    public class BiDictionary<TLeft, TRight> : IDictionary<TLeft, TRight>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiDictionary{TLeft,TRight}"/> class.
        /// </summary>
        public BiDictionary(IDictionary<TLeft, TRight> dictionary, IEqualityComparer<TLeft> leftComparer = null, 
            IEqualityComparer<TRight> rightComparer = null)
            : this(dictionary.Count, leftComparer, rightComparer)
        {
            foreach (var item in dictionary)
            {
                Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BiDictionary{TLeft,TRight}"/> class.
        /// </summary>
        public BiDictionary(int capacity = 0, IEqualityComparer<TLeft> leftComparer = null, IEqualityComparer<TRight> rightComparer = null)
        {
            Left = new DictionarySide<TLeft, TRight>(capacity, leftComparer,
                                                     (right, left) => Right.Dictionary[right] = left,
                                                     (oldRight, right, left) => Right.Replace(oldRight, right, left));

            Right = new DictionarySide<TRight, TLeft>(capacity, rightComparer,
                                                      (left, right) => Left.Dictionary[left] = right,
                                                      (oldLeft, left, right) => Left.Replace(oldLeft, left, right));
        }

        /// <summary>
        ///   Gets the key side of bidirectional dictionary, where the key is <typeparamref name="TLeft" /> and the value is <typeparamref
        ///    name="TRight" /> .
        /// </summary>
        public DictionarySide<TLeft, TRight> Left { get; private set; }

        /// <summary>
        ///   Gets the value side of the bidirectional dictionary, where the key is <typeparamref name="TRight" /> and the value is <typeparamref
        ///    name="TLeft" /> .
        /// </summary>
        public DictionarySide<TRight, TLeft> Right { get; private set; }

        /// <summary>
        ///   Determines whether or not the dictionary contains the specified value.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns> True if the dictionary contains the specified value; otherwise, false. </returns>
        public bool ContainsValue(TRight value)
        {
            return Left.Dictionary.ContainsValue(value);
        }

        /// <summary>
        ///   Returns the key associated with the value.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <param name="key"> When the method returns and the value found in the dictionary, the key will be the key associated with the specified value. Otherwise, key will be set to the default value of <typeparamref
        ///    name="TLeft" /> . </param>
        /// <returns> True if the value and key were found; otherwise, false. </returns>
        public bool TryGetKey(TRight value, out TLeft key)
        {
            return Right.Dictionary.TryGetValue(value, out key);
        }

        #region Implementation of IEnumerable

        /// <summary>
        ///   Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection. </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<TLeft, TRight>> GetEnumerator()
        {
            return Left.Dictionary.GetEnumerator();
        }

        /// <summary>
        ///   Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<KeyValuePair<TLeft,TRight>>

        /// <summary>
        ///   Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <param name="item"> The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        void ICollection<KeyValuePair<TLeft, TRight>>.Add(KeyValuePair<TLeft, TRight> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        ///   Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        public void Clear()
        {
            Left.Dictionary.Clear();
            Right.Dictionary.Clear();
        }

        /// <summary>
        ///   Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <returns> true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. </returns>
        /// <param name="item"> The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        bool ICollection<KeyValuePair<TLeft, TRight>>.Contains(KeyValuePair<TLeft, TRight> item)
        {
            return ((ICollection<KeyValuePair<TLeft, TRight>>) Left.Dictionary).Contains(item);
        }

        /// <summary>
        ///   Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" /> , starting at a particular <see
        ///    cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array"> The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see
        ///    cref="T:System.Collections.Generic.ICollection`1" /> . The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        /// <param name="arrayIndex"> The zero-based index in <paramref name="array" /> at which copying begins. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="array" />
        ///   is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="arrayIndex" />
        ///   is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="array" />
        ///   is multidimensional.-or-The number of elements in the source
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is greater than the available space from
        ///   <paramref name="arrayIndex" />
        ///   to the end of the destination
        ///   <paramref name="array" />
        ///   .-or-Type
        ///   cannot be cast automatically to the type of the destination
        ///   <paramref name="array" />
        ///   .</exception>
        void ICollection<KeyValuePair<TLeft, TRight>>.CopyTo(KeyValuePair<TLeft, TRight>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TLeft, TRight>>) Left.Dictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///   Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <returns> true if <paramref name="item" /> was successfully removed from the <see
        ///    cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. This method also returns false if <paramref
        ///    name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
        /// <param name="item"> The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        bool ICollection<KeyValuePair<TLeft, TRight>>.Remove(KeyValuePair<TLeft, TRight> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        ///   Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <returns> The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
        public int Count
        {
            get { return Left.Dictionary.Count; }
        }

        /// <summary>
        ///   Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns> true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false. </returns>
        bool ICollection<KeyValuePair<TLeft, TRight>>.IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IDictionary<TLeft,TRight>

        /// <summary>
        ///   Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <returns> true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false. </returns>
        /// <param name="key"> The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" /> . </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="key" />
        ///   is null.</exception>
        public bool ContainsKey(TLeft key)
        {
            return Left.Dictionary.ContainsKey(key);
        }

        /// <summary>
        ///   Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" /> .
        /// </summary>
        /// <param name="key"> The object to use as the key of the element to add. </param>
        /// <param name="value"> The object to use as the value of the element to add. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="key" />
        ///   is null.</exception>
        /// <exception cref="T:System.ArgumentException">An element with the same key already exists in the
        ///   <see cref="T:System.Collections.Generic.IDictionary`2" />
        ///   .</exception>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.IDictionary`2" />
        ///   is read-only.</exception>
        public void Add(TLeft key, TRight value)
        {
            Requires.That(value)
                .IsMet(!Right.Dictionary.ContainsKey(value),
                       "The specified value has already been mapped to another key.");

            Left.Dictionary.Add(key, value);
            Right.Dictionary.Add(value, key);
        }

        /// <summary>
        ///   Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" /> .
        /// </summary>
        /// <returns> true if the element is successfully removed; otherwise, false. This method also returns false if <paramref
        ///    name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
        /// <param name="key"> The key of the element to remove. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="key" />
        ///   is null.</exception>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.IDictionary`2" />
        ///   is read-only.</exception>
        public bool Remove(TLeft key)
        {
            if (Left.Dictionary.ContainsKey(key) && Right.Dictionary.ContainsKey(Left.Dictionary[key]))
            {
                Right.Dictionary.Remove(Left.Dictionary[key]);
                Left.Dictionary.Remove(key);

                return true;
            }

            return false;
        }

        /// <summary>
        ///   Gets the value associated with the specified key.
        /// </summary>
        /// <returns> true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false. </returns>
        /// <param name="key"> The key whose value to get. </param>
        /// <param name="value"> When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref
        ///    name="value" /> parameter. This parameter is passed uninitialized. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="key" />
        ///   is null.</exception>
        public bool TryGetValue(TLeft key, out TRight value)
        {
            return Left.Dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        ///   Gets or sets the element with the specified key.
        /// </summary>
        /// <returns> The element with the specified key. </returns>
        /// <param name="key"> The key of the element to get or set. </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="key" />
        ///   is null.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and
        ///   <paramref name="key" />
        ///   is not found.</exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and the
        ///   <see cref="T:System.Collections.Generic.IDictionary`2" />
        ///   is read-only.</exception>
        public TRight this[TLeft key]
        {
            get { return Left[key]; }
            set { Left[key] = value; }
        }

        /// <summary>
        ///   Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see
        ///    cref="T:System.Collections.Generic.IDictionary`2" /> .
        /// </summary>
        /// <returns> An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see
        ///    cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
        public ICollection<TLeft> Keys
        {
            get { return Left.Dictionary.Keys; }
        }

        /// <summary>
        ///   Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see
        ///    cref="T:System.Collections.Generic.IDictionary`2" /> .
        /// </summary>
        /// <returns> An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see
        ///    cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
        public ICollection<TRight> Values
        {
            get { return Left.Dictionary.Values; }
        }

        #endregion

        #region Nested type: DictionarySide

        /// <summary>
        ///   Contains one side of the bidirectional dictionary.
        /// </summary>
        /// <typeparam name="T1"> </typeparam>
        /// <typeparam name="T2"> </typeparam>
        public class DictionarySide<T1, T2>
        {
            internal readonly Dictionary<T1, T2> Dictionary;

            private readonly Action<T2, T1> _insertOther;

            private readonly Action<T2, T2, T1> _replaceOther;

            internal DictionarySide(int capacity, IEqualityComparer<T1> comparer,
                                    Action<T2, T1> insertOther, Action<T2, T2, T1> replaceOther)
            {
                Dictionary = new Dictionary<T1, T2>(capacity, comparer);
                _insertOther = insertOther;
                _replaceOther = replaceOther;
            }

            /// <summary>
            ///   Gets or sets the value by key.
            /// </summary>
            /// <param name="key"> The key. </param>
            /// <returns> The value associated with the key. </returns>
            public T2 this[T1 key]
            {
                get { return Dictionary[key]; }
                set
                {
                    if (Dictionary.ContainsKey(key)) _replaceOther(Dictionary[key], value, key);
                    else _insertOther(value, key);
                }
            }

            internal void Replace(T1 removeKey, T1 key, T2 value)
            {
                Dictionary.Remove(removeKey);
                Dictionary[key] = value;
            }
        }

        #endregion
    }
}