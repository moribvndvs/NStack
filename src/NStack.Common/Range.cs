#region header

// <copyright file="Range.cs" company="mikegrabski.com">
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
using System.Collections.Generic;

namespace NStack
{
    /// <summary>
    ///     A type that represents a range of values, bounded by a minimum and maxixum value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T> : IEquatable<Range<T>> where T : IComparable<T>
    {
        private readonly T _max;

        private readonly T _min;

        /// <summary>
        ///     Inititalizes a new instance of <see cref="Range{T}" />.
        /// </summary>
        /// <param name="min">The minimum bounds.</param>
        /// <param name="max">THe maximum bounds.</param>
        public Range(T min, T max)
        {
            Requires.That(min, "min").IsNotNull();
            Requires.That(max, "max").IsNotNull();

            if (min.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException("The minimum value cannot be less than the maximum value.");

            _min = min;
            _max = max;
        }

        /// <summary>
        ///     Gets the value of the minimum bounds.
        /// </summary>
        public T Minimum
        {
            get { return _min; }
        }

        /// <summary>
        ///     Gets the value of the maximum bounds.
        /// </summary>
        public T Maximum
        {
            get { return _max; }
        }

        /// <summary>
        /// Determines whether <paramref name="other"/> is equivalent to the current <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Range{T}"/> to compare.</param>
        /// <returns>True if the ranges are equivalent; otherwise, false.</returns>
        public virtual bool Equals(Range<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(_max, other._max) &&
                   EqualityComparer<T>.Default.Equals(_min, other._min);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Range<T>;
            return other != null && Equals(other);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_max)*397) ^
                       EqualityComparer<T>.Default.GetHashCode(_min);
            }
        }

        /// <summary>
        /// Operator to deterine if two ranges are equivalent.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>True if <paramref name="right"/> and <paramref name="left"/> are equivalent; otherwise, false.</returns>
        public static bool operator ==(Range<T> left, Range<T> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Operator to determine if two ranges are not equal.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns>True if <paramref name="right"/> and <paramref name="left"/> are not equivalent; otherwise, false.</returns>
        public static bool operator !=(Range<T> left, Range<T> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Returns whether or not <paramref name="other" /> intersects the current range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns>True if the ranges intersect; otherwise, false.</returns>
        public virtual bool Intersects(Range<T> other)
        {
            return Intersects(this, other);
        }

        /// <summary>
        ///     Returns whether or not the specified <paramref name="value" /> exists within the bounds of the range.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     True if <paramref name="value" /> exists within the range.
        /// </returns>
        public virtual bool Contains(T value)
        {
            return Minimum.CompareTo(value) <= 0 && Maximum.CompareTo(value) >= 0;
        }

        /// <summary>
        ///     Returns whether or not the two ranges intersect.
        /// </summary>
        /// <param name="first">The first range.</param>
        /// <param name="second">The second range.</param>
        /// <returns>True if the ranges intersect; otherwise, false.</returns>
        public static bool Intersects(Range<T> first, Range<T> second)
        {
            return !(second.Maximum.CompareTo(first.Minimum) <= 0 || first.Maximum.CompareTo(second.Minimum) <= 0);
        }
    }
}