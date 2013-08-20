#region header

// <copyright file="SemanticVersion.cs" company="mikegrabski.com">
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NStack
{
    /// <summary>
    ///     Represents a software version based on Semantic Versioning.
    /// </summary>
    /// <remarks>
    ///     Refer to the specification outlined at http://semver.org.
    /// </remarks>
    public class SemanticVersion : IComparable<SemanticVersion>, IComparable, IFormattable, IEquatable<SemanticVersion>
    {
        #region Stage enum

        /// <summary>
        ///     Indicates common pre-release stages for <see cref="SemanticVersion" /> .
        /// </summary>
        public enum Stage
        {
            /// <summary>
            ///     Not a pre-release stage (a final or RTW release).
            /// </summary>
            None,

            /// <summary>
            ///     A preview or pre-alpha release stage.
            /// </summary>
            Pre,

            /// <summary>
            ///     An alpha release stage.
            /// </summary>
            Alpha,

            /// <summary>
            ///     A beta release stage.
            /// </summary>
            Beta,

            /// <summary>
            ///     A release candidate stage.
            /// </summary>
            Rc
        }

        #endregion

        private static readonly Regex ParseExpression =
            new Regex(
                @"^(?<ma>[0-9]+)\.(?<mi>[0-9]+)(\.(?<pa>[0-9]+))?(\-(?<pr>[a-zA-Z0-9\.]+))?(\+(?<b>[A-Za-z0-9\.]+))?$");

        /// <summary>
        ///     Represents an unspecified semantic version.
        /// </summary>
        public static readonly SemanticVersion Unspecified = new SemanticVersion(0, 0, 0);

        /// <summary>
        ///     Represents the maximum semantic version (excluding build).
        /// </summary>
        public static readonly SemanticVersion MaxValue = new SemanticVersion(int.MaxValue, int.MaxValue, int.MaxValue);

        /// <summary>
        ///     Initializes a new instance of <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="major"> The major version. </param>
        /// <param name="minor"> The minor version. </param>
        /// <param name="patch"> The patch version. </param>
        public SemanticVersion(int major, int minor, int patch)
            : this(major, minor, patch, preRelease: null, build: null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="major"> The major version. </param>
        /// <param name="minor"> The minor version. </param>
        /// <param name="patch"> The patch version. </param>
        /// <param name="preRelease"> The pre-release version. </param>
        /// <param name="build"> The build version. </param>
        public SemanticVersion(int major, int minor, int patch, string preRelease = null, string build = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            ValidateLabelArgument("preRelease", preRelease);

            PreRelease = string.IsNullOrWhiteSpace(preRelease) ? null : preRelease;

            ValidateLabelArgument("build", build);

            Build = string.IsNullOrWhiteSpace(build) ? null : build;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="major"> The major version. </param>
        /// <param name="minor"> The minor version. </param>
        /// <param name="patch"> The patch version. </param>
        /// <param name="stage"> The pre-release stage. </param>
        /// <param name="step"> The step in the pre-release stage. </param>
        /// <param name="build"> The build version. </param>
        public SemanticVersion(int major, int minor, int patch = 0, Stage stage = Stage.None,
                               int? step = null, string build = null)
        {
            Major = major;
            Minor = minor;
            Patch = patch;

            ValidateLabelArgument("build", build);
            Build = build;

            if (stage != Stage.None)
            {
                string s = stage.ToString().ToLowerInvariant();
                PreRelease = step.HasValue && step.Value > 0
                                 ? s + "." + step.Value.ToString(CultureInfo.InvariantCulture)
                                 : s;
            }
        }

        /// <summary>
        ///     Gets the major version.
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        ///     Gets the minor version.
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        ///     Gets the patch version.
        /// </summary>
        public int Patch { get; private set; }

        /// <summary>
        ///     Gets the pre-release version.
        /// </summary>
        public string PreRelease { get; private set; }

        /// <summary>
        ///     Gets the build version.
        /// </summary>
        public string Build { get; private set; }

        #region IComparable Members

        /// <summary>
        ///     Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than
        ///     <paramref
        ///         name="obj" />
        ///     . Zero This instance is equal to <paramref name="obj" /> . Greater than zero This instance is greater than
        ///     <paramref
        ///         name="obj" />
        ///     .
        /// </returns>
        /// <param name="obj"> An object to compare with this instance. </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj" />
        ///     is not the same type as this instance.
        /// </exception>
        /// <filterpriority>2</filterpriority>
        int IComparable.CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (!(obj is SemanticVersion)) throw new ArgumentException("Must be of type SemanticVersion.", "obj");

            return CompareTo(obj as SemanticVersion);
        }

        #endregion

        #region IComparable<SemanticVersion> Members

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the
        ///     <paramref
        ///         name="other" />
        ///     parameter.Zero This object is equal to <paramref name="other" /> . Greater than zero This object is greater than
        ///     <paramref
        ///         name="other" />
        ///     .
        /// </returns>
        /// <param name="other"> An object to compare with this object. </param>
        public virtual int CompareTo(SemanticVersion other)
        {
            if (other == null) return 1;

            if (Major != other.Major) return Major.CompareTo(other.Major);
            if (Minor != other.Minor) return Minor.CompareTo(other.Minor);
            if (Patch != other.Patch) return Patch.CompareTo(other.Patch);

            int i = CompareLabels(PreRelease, other.PreRelease, true);

            if (i != 0) return i;

            return CompareLabels(Build, other.Build, false);
        }

        #endregion

        #region IEquatable<SemanticVersion> Members

        /// <summary>
        ///     Determines whether the specified <see cref="SemanticVersion" /> equals the current <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="other"> The other version to compare. </param>
        /// <returns> True if the versions are equivalent; otherwise, false. </returns>
        public virtual bool Equals(SemanticVersion other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Build, other.Build, StringComparison.OrdinalIgnoreCase) && Major == other.Major &&
                   Minor == other.Minor &&
                   Patch == other.Patch &&
                   string.Equals(PreRelease, other.PreRelease, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region IFormattable Members

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="format"> A format identifier. </param>
        /// <param name="formatProvider"> The format provider. </param>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> , formatted according to
        ///     <paramref
        ///         name="format" />
        ///     .
        /// </returns>
        /// <exception cref="FormatException">
        ///     <paramref name="format" />
        ///     is an unknown format identifier.
        /// </exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "f";

            formatProvider = formatProvider ?? CultureInfo.CurrentCulture;
            var builder = new StringBuilder();

            switch (format)
            {
                case "s": // significant version, omitting pre-release and build
                    builder.AppendFormat(formatProvider, "{0}.{1}.{2}", Major, Minor, Patch);
                    break;
                case "S": // significant version, omitting pre-release, build, and patch if patch is 0
                    builder.AppendFormat(Patch == 0 ? "{0}.{1}" : "{0}.{1}.{2}", Major, Minor, Patch);
                    break;
                case "f": // full version, including pre-release and build
                    builder.AppendFormat(formatProvider, "{0}.{1}.{2}", Major, Minor, Patch);
                    if (!string.IsNullOrEmpty(PreRelease)) builder.AppendFormat(formatProvider, "-{0}", PreRelease);
                    if (!string.IsNullOrEmpty(Build)) builder.AppendFormat(formatProvider, "+{0}", Build);
                    break;
                case "F": // full version, including pre-release but omitting build
                    builder.AppendFormat(formatProvider, "{0}.{1}.{2}", Major, Minor, Patch);
                    if (!string.IsNullOrEmpty(PreRelease)) builder.AppendFormat(formatProvider, "-{0}", PreRelease);
                    break;
                default:
                    throw new FormatException();
            }

            return builder.ToString();
        }

        #endregion

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> .
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> ; otherwise, false.
        /// </returns>
        /// <param name="obj">
        ///     The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" /> .
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SemanticVersion) obj);
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
            unchecked
            {
                int hashCode = (Build != null ? Build.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Major;
                hashCode = (hashCode*397) ^ Minor;
                hashCode = (hashCode*397) ^ Patch;
                hashCode = (hashCode*397) ^ (PreRelease != null ? PreRelease.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        ///     Determines whether or not two <see cref="SemanticVersion" /> instances are equivalent.
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns> True if the instances are equivalent; otherwise, false. </returns>
        public static bool operator ==(SemanticVersion left, SemanticVersion right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Determines whether or not two <see cref="SemanticVersion" /> instances are not equivalent.
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns> True if the instances are not equivalent; otherwise, false. </returns>
        public static bool operator !=(SemanticVersion left, SemanticVersion right)
        {
            return !Equals(left, right);
        }


        /// <summary>
        ///     Determines if <paramref name="left" /> is greater than <paramref name="right" /> .
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns>
        ///     True if <paramref name="left" /> is greater than <paramref name="right" /> ; otherwise, false.
        /// </returns>
        public static bool operator >(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        ///     Determines if <paramref name="left" /> is less than <paramref name="right" /> .
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns>
        ///     True if <paramref name="left" /> is less than <paramref name="right" /> ; otherwise, false.
        /// </returns>
        public static bool operator <(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///     Determines if <paramref name="left" /> is greater than or equal to <paramref name="right" /> .
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns>
        ///     True if <paramref name="left" /> is greater than <paramref name="right" /> ; otherwise, false.
        /// </returns>
        public static bool operator >=(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///     Determines if <paramref name="left" /> is less than or equal to <paramref name="right" /> .
        /// </summary>
        /// <param name="left"> The left instance. </param>
        /// <param name="right"> The right instance. </param>
        /// <returns>
        ///     True if <paramref name="left" /> is less than <paramref name="right" /> ; otherwise, false.
        /// </returns>
        public static bool operator <=(SemanticVersion left, SemanticVersion right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        ///     Validates a label argument as having only the following characters: [A-Za-z0-9.]
        /// </summary>
        /// <param name="name"> The name of the argument. </param>
        /// <param name="value"> The value of the argument. </param>
        /// <exception cref="ArgumentException">
        ///     The
        ///     <paramref name="value" />
        ///     contains an invalid character.
        /// </exception>
        private static void ValidateLabelArgument(string name, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            if (value.Any(c => !char.IsLetterOrDigit(c) && c != '.'))
            {
                throw new ArgumentException(name);
            }
        }

        /// <summary>
        ///     Compare two labels.
        /// </summary>
        /// <param name="value"> The current value. </param>
        /// <param name="other"> The other value. </param>
        /// <param name="preRelease"> Whether or not the label is a pre-release label. </param>
        /// <returns> The relative difference between the two values. </returns>
        private static int CompareLabels(string value, string other, bool preRelease)
        {
            if (string.Compare(value, other, StringComparison.InvariantCultureIgnoreCase) == 0) return 0;

            if (string.IsNullOrEmpty(value)) return preRelease ? 1 : -1;
            if (string.IsNullOrEmpty(other)) return preRelease ? -1 : 1;

            string[] valueSplit = value.Split(new[] {'.'});
            string[] otherSplit = other.Split(new[] {'.'});
            int compare = 0;

            for (int i = 0; i < valueSplit.Length; i++)
            {
                if (i >= otherSplit.Length) break;

                string first = valueSplit[i];
                string second = otherSplit[i];

                if (preRelease && !string.Equals(first, second, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals("pre", first, StringComparison.OrdinalIgnoreCase)) return -1;
                    if (string.Equals("pre", second, StringComparison.OrdinalIgnoreCase)) return 1;
                }

                compare = ComparePotentialIntegers(first, second);

                if (compare != 0) break;
            }

            if (compare == 0 && valueSplit.Length != otherSplit.Length)
                return valueSplit.Length > otherSplit.Length ? 1 : -1;

            if (compare < 0) return -1;

            return compare > 0 ? 1 : compare;
        }

        /// <summary>
        ///     Compares two values, first as integers if they appear to be numeric, otherwise as strings.
        /// </summary>
        /// <param name="firstString"> The first string. </param>
        /// <param name="secondString"> The second string. </param>
        /// <returns> The relative difference between the two strings. </returns>
        private static int ComparePotentialIntegers(string firstString, string secondString)
        {
            int firstNumber;
            int secondNumber;

            if (int.TryParse(firstString, out firstNumber) && int.TryParse(secondString, out secondNumber))
            {
                return firstNumber.CompareTo(secondNumber);
            }

            return string.Compare(firstString, secondString, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> .
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> .
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> .
        /// </summary>
        /// <param name="format"> A format identifier. </param>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current <see cref="SemanticVersion" /> , formatted according to
        ///     <paramref
        ///         name="format" />
        ///     .
        /// </returns>
        /// <exception cref="FormatException">
        ///     <paramref name="format" />
        ///     is an unknown format identifier.
        /// </exception>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        /// <summary>
        ///     Attempts to parse a <see cref="SemanticVersion" /> from a string.
        /// </summary>
        /// <param name="value"> The string containing the semantic version. </param>
        /// <param name="version">
        ///     The resulting <see cref="SemanticVersion" /> .
        /// </param>
        /// <returns>
        ///     True if a <see cref="SemanticVersion" /> was found in <paramref name="value" /> and was assigned to
        ///     <paramref
        ///         name="version" />
        ///     ; otherwise, false.
        /// </returns>
        public static bool TryParse(string value, out SemanticVersion version)
        {
            version = null;

            if (string.IsNullOrEmpty(value)) return false;

            try
            {
                version = Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Parses a <see cref="SemanticVersion" /> from a string.
        /// </summary>
        /// <param name="value"> The string containing the semantic version. </param>
        /// <returns>
        ///     The <see cref="SemanticVersion" /> parsed from <paramref name="value" /> .
        /// </returns>
        public static SemanticVersion Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", "value");

            Match match = ParseExpression.Match(value);

            if (!match.Success)
                throw new ArgumentException("String does not contain a valid semantic version.", "value");

            int ma = Convert.ToInt32(match.Groups["ma"].Value);
            int mi = Convert.ToInt32(match.Groups["mi"].Value);
            int pa = match.Groups["pa"].Success ? Convert.ToInt32(match.Groups["pa"].Value) : 0;
            string pr = match.Groups["pr"].Value;
            string b = match.Groups["b"].Value;

            return new SemanticVersion(ma, mi, pa, pr, b);
        }
    }
}