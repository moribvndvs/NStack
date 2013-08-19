#region header

// -----------------------------------------------------------------------
//  <copyright file="Flake.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.ComponentModel;
using System.Globalization;

namespace NStack
{
    /// <summary>
    ///     Represents an identifier that is unique and sequential, and can be generated in a non-coordinated, distributed environment.
    /// </summary>
    [Serializable, TypeConverter(typeof(ComponentModel.FlakeConverter))]
    public struct Flake : IEquatable<Flake>, IComparable<Flake>, IComparable, IFormattable
    {
        /// <summary>
        ///     A <see cref="Flake" /> that has not been assigned a unique identifier yet.
        /// </summary>
        public static readonly Flake Unassigned = new Flake();

        private readonly decimal _value;

        /// <summary>
        ///     Initializes a new instance of <see cref="Flake" />.
        /// </summary>
        public Flake(decimal value) : this()
        {
            Requires.That(value, "value").IsGreaterThanOrEqualTo(0);

            _value = decimal.Truncate(value);
        }

        #region Equality members

        #region IComparable Members

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than
        ///     <paramref
        ///         name="obj" />
        ///     . Zero This instance is equal to <paramref name="obj" />. Greater than zero This instance is greater than
        ///     <paramref
        ///         name="obj" />
        ///     .
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj" /> is not the same type as this instance.
        /// </exception>
        int IComparable.CompareTo(object obj)
        {
            return CompareTo((Flake) obj);
        }

        #endregion

        #region IComparable<Flake> Members

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the
        ///     <paramref
        ///         name="other" />
        ///     parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than
        ///     <paramref
        ///         name="other" />
        ///     .
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Flake other)
        {
            return _value.CompareTo(other._value);
        }

        #endregion

        #region IEquatable<Flake> Members

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Flake other)
        {
            return _value == other._value;
        }

        #endregion

        /// <summary>
        ///     Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Flake && Equals((Flake) obj);
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        ///     Returns whether or not two <see cref="Flake" /> values are equal.
        /// </summary>
        /// <param name="left">The left flake.</param>
        /// <param name="right">THe right flake.</param>
        /// <returns>
        ///     True if <paramref name="left" /> is equal to <paramref name="right" />.
        /// </returns>
        public static bool operator ==(Flake left, Flake right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Returns whether or not two <see cref="Flake" /> values are not equal.
        /// </summary>
        /// <param name="left">The left flake.</param>
        /// <param name="right">THe right flake.</param>
        /// <returns>
        ///     True if <paramref name="left" /> is equal to <paramref name="right" />.
        /// </returns>
        public static bool operator !=(Flake left, Flake right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Flake left, Flake right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Flake left, Flake right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Flake left, Flake right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Flake left, Flake right)
        {
            return left.CompareTo(right) <= 0;
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Flake(decimal value)
        {
            return new Flake(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Flake(string value)
        {
            return Parse(value);
        }

        /// <summary>
        ///     Determines if the current <see cref="Flake" /> value represents an unassigned identifier.
        /// </summary>
        /// <returns>True if the value is unassigned; otherwise, false.</returns>
        public bool IsUnassigned()
        {
            return Equals(Unassigned);
        }

        /// <summary>
        ///     Determines if the specified value represents an unassigned identifier.
        /// </summary>
        /// <param name="value">The value/</param>
        /// <returns>
        ///     True if <paramref name="value" /> is unassigned; otherwise, false.
        /// </returns>
        public static bool IsUnassigned(Flake value)
        {
            return Equals(Unassigned, value);
        }

        /// <summary>
        /// Returns the flake as a string representation.
        /// </summary>
        /// <returns>
        /// The string representation of the flake value.
        /// </returns>
        public override string ToString()
        {
            return ToString(null);
        }

        /// <summary>
        /// Returns the flake as a string representation.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The string representation of the flake value.
        /// </returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>
        /// The value of the current instance in the specified format.
        /// </returns>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable"/> implementation. </param><param name="formatProvider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system. </param>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            format = format ?? "h";

            switch (format)
            {
                case "h":
                    return NumberBases.ToBase24String(_value);
                case "d":
                    return _value.ToString("#", formatProvider);
                default:
                    throw new FormatException(string.Format("Unrecognized format string: {0}.", format));
            }
        }

        /// <summary>
        /// Parses an encoded string for a flake value.
        /// </summary>
        /// <param name="value">The encoded flake value.</param>
        /// <returns>A <see cref="Flake"/> representing the parsed value.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">The value is an empty string.</exception>
        public static Flake Parse(string value)
        {
            if (value == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value)) throw new ArgumentException();
            if (value == Unassigned.ToString()) return Unassigned;

            var d = NumberBases.FromBase24String(value);

            if (!d.HasValue) throw new FormatException("The encoded flake value was invalid.");

            return new Flake(d.Value);
        }

        /// <summary>
        /// Attempts to parse an encoded flake value without causing an exception.
        /// </summary>
        /// <param name="value">The encoded flake value.</param>
        /// <param name="result">The variable receiving the result.</param>
        /// <returns>True if a value was parsed successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out Flake result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = new Flake();
                return false;
            }

            try
            {
                result = Parse(value);
                return true;
            }
            catch (Exception)
            {
                result = new Flake();
                return false;
            }
        }
    }
}