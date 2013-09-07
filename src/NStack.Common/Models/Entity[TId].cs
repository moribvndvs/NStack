#region header

// <copyright file="Entity[TId].cs" company="mikegrabski.com">
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

namespace NStack.Models
{
    /// <summary>
    ///     A generic root entity with a strongly typed ID.
    /// </summary>
    /// <typeparam name="TId"> The type of the ID property. </typeparam>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        private int? _hashCode;

        /// <summary>
        ///     Gets the ID of the entity.
        /// </summary>
        public virtual TId Id { get; protected set; }

        #region IEquatable<Entity<TId>> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other"> An object to compare with this object. </param>
        public virtual bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetTypeUnproxied() != other.GetTypeUnproxied()) return false;
            return IsEquivalentTo(other);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is Entity<TId>) return Equals((Entity<TId>) obj);
            return true;
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" /> .
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
// ReSharper disable NonReadonlyFieldInGetHashCode
            if (_hashCode.HasValue) return _hashCode.Value;


// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
            if (IsTransient()) _hashCode = base.GetHashCode();
// ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
            else
            {
                unchecked
                {
                    _hashCode = (GetType().GetHashCode()*397) ^ Id.GetHashCode();
                }
            }

            return _hashCode.Value;
// ReSharper restore NonReadonlyFieldInGetHashCode
        }

        /// <summary>
        ///     Returns whether or not the instance is transient; that is, whether or not it has been attached to a persistence medium.
        /// </summary>
        /// <returns> True if the instance is transient; otherwise, false. </returns>
        public virtual bool IsTransient()
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            return Id == null || default(TId).Equals(Id);
// ReSharper restore CompareNonConstrainedGenericWithNull
        }

        /// <summary>
        ///     Returns the expected type, in the event that the current instance is a proxy.
        /// </summary>
        /// <returns> The unproxied type. </returns>
        public virtual Type GetTypeUnproxied()
        {
            return GetType();
        }

        private bool IsEquivalentTo(Entity<TId> compareTo)
        {
            return !IsTransient() && !compareTo.IsTransient() && Id.Equals(compareTo.Id);
        }
    }
}