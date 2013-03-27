#region header

// <copyright file="SemanticVersionRange.cs" company="mikegrabski.com">
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

namespace NStack
{
    /// <summary>
    ///     Represents a range of <see cref="SemanticVersion" /> values, used typically to represent and/or validate valid dependencies.
    /// </summary>
    public class SemanticVersionRange : Range<SemanticVersion>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="SemanticVersionRange" />.
        /// </summary>
        /// <param name="range">A string-representation of the range of versions.</param>
        public SemanticVersionRange(string range)
            : this(ParseRangeString(range).Item1, ParseRangeString(range).Item2)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="SemanticVersionRange" />.
        /// </summary>
        /// <param name="min">The minimum version.</param>
        /// <param name="max">The maximum version.</param>
        public SemanticVersionRange(SemanticVersion min, SemanticVersion max)
            : base(min, max)
        {
            if (Minimum is ExclusiveVersion) ((ExclusiveVersion) Minimum).IsLowerBound = true;
            if (Maximum is ExclusiveVersion) ((ExclusiveVersion) Maximum).IsLowerBound = false;
        }

        private bool IsMinimumExclusive()
        {
            return Minimum is ExclusiveVersion;
        }

        private bool IsMaximumExclusive()
        {
            return Maximum is ExclusiveVersion;
        }

        private static bool ShallowEquals(SemanticVersion a, SemanticVersion b)
        {
            return new SemanticVersion(a.Major, a.Minor, a.Patch).Equals(new SemanticVersion(b.Major, b.Minor, b.Patch));
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            if (!IsMinimumExclusive() && !IsMaximumExclusive() && Maximum == SemanticVersion.MaxValue)
                return Minimum.ToString("S");

            if (!IsMinimumExclusive() && !IsMaximumExclusive() && Minimum == Maximum)
                return string.Format("[{0:S}]", Minimum);

            return string.Format("{0}{1},{2}{3}", IsMinimumExclusive() ? "(" : "[",
                                 ShallowEquals(Minimum, SemanticVersion.Unspecified)
                                     ? string.Empty
                                     : Minimum.ToString("S"),
                                 ShallowEquals(Maximum, SemanticVersion.MaxValue) ? string.Empty : Maximum.ToString("S"),
                                 IsMaximumExclusive() ? ")" : "]");
        }

        /// <summary>
        ///     Parses a <see cref="SemanticVersionRange" /> from a string.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     A <see cref="SemanticVersionRange" />.
        /// </returns>
        public static SemanticVersionRange Parse(string range)
        {
            return new SemanticVersionRange(range);
        }

        /// <summary>
        ///     Attempts to safely parse a <see cref="SemanticVersionRange" /> from a string.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="result">
        ///     The resulting <see cref="SemanticVersionRange" />.
        /// </param>
        /// <returns>
        ///     True if <paramref name="range" /> is successfully parsed and the value assigned to <paramref name="result" />; otherwise, false.
        /// </returns>
        public static bool TryParse(string range, out SemanticVersionRange result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(range)) return false;

            try
            {
                result = Parse(range);

                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static Tuple<SemanticVersion, SemanticVersion> ParseRangeString(string range)
        {
            Requires.That(range, "range").IsNotNullOrEmpty();

            string[] s = range.Split(',');

            if (s.Length > 2) throw new FormatException();

            if (s.Length == 2)
            {
                bool minInclusive = s[0].StartsWith("[");
                bool maxInclusive = s[1].EndsWith("]");

                string mins = TrimSymbols(s[0]);
                string maxs = TrimSymbols(s[1]);

                SemanticVersion min = string.IsNullOrEmpty(mins)
                                          ? SemanticVersion.Unspecified
                                          : SemanticVersion.Parse(mins);
                SemanticVersion max = string.IsNullOrEmpty(maxs)
                                          ? SemanticVersion.MaxValue
                                          : SemanticVersion.Parse(maxs);

                return new Tuple<SemanticVersion, SemanticVersion>(
                    minInclusive ? Inclusive(min) : Exclusive(min),
                    maxInclusive ? Inclusive(max) : Exclusive(max)
                    );
            }

            SemanticVersion value = SemanticVersion.Parse(TrimSymbols(s[0]));

            if (s[0].StartsWith("(") || s[0].EndsWith(")")) throw new FormatException();

            if (s[0].StartsWith("[") && s[0].EndsWith("]"))
                return new Tuple<SemanticVersion, SemanticVersion>(Inclusive(value), Inclusive(value));

            return new Tuple<SemanticVersion, SemanticVersion>(Inclusive(value), Inclusive(SemanticVersion.MaxValue));
        }

        private static string TrimSymbols(string value)
        {
            return value.Trim('[', '(', ')', ']');
        }

        /// <summary>
        ///     Creates a <see cref="SemanticVersion" /> used as an exclusive bounds in a range.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The version.</returns>
        public static SemanticVersion Exclusive(SemanticVersion version)
        {
            return new ExclusiveVersion(version);
        }

        /// <summary>
        ///     Creates a <see cref="SemanticVersion" /> used as an inclusive bounds in a range.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The version.</returns>
        public static SemanticVersion Inclusive(SemanticVersion version)
        {
            return version;
        }

        internal class ExclusiveVersion : SemanticVersion
        {
            internal bool? IsLowerBound;

            public ExclusiveVersion(SemanticVersion version)
                : base(version.Major, version.Minor, version.Patch)
            {
            }

            public override bool Equals(SemanticVersion other)
            {
                var e = other as ExclusiveVersion;

                return base.Equals(other) && e != null && e.IsLowerBound == IsLowerBound;
            }

            public override int CompareTo(SemanticVersion other)
            {
                int value = base.CompareTo(other);


                return value == 0 && IsLowerBound.HasValue
                           ? (IsLowerBound.Value ? 1 : -1)
                           : value;
            }
        }
    }
}