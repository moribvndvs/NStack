#region header

// <copyright file="NumberBases.cs" company="mikegrabski.com">
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
    ///     A utility class for encoding and decoding numbers using different bases.
    /// </summary>
    public static class NumberBases
    {
        /// <summary>
        ///     Digits used in the Base24 number system
        /// </summary>
        private const string Base24Digits = "bcdfghjkmpqrtvwxy2346789";

        /// <summary>
        ///     Bits per digit in the Base24 number system
        /// </summary>
        private const int Base24Bits = 24;

        /// <summary>
        ///     Encodes a decimal into a base24-encoded string.
        /// </summary>
        /// <param name="value">The value to be encoded.</param>
        /// <returns>The base24-encoded string.</returns>
        public static string ToBase24String(decimal value)
        {
            int digit = 0;
            var result = new char[22]; // decimal.MaxValue fits into 21 base24 characters
            while (value > 0)
            {
                digit++;
                result[22 - digit] = Base24Digits[(int) (value%Base24Bits)];
                value = Math.Floor(value/Base24Bits);
            }
            return new string(result, 22 - digit, digit); // we assembled the string from the end
        }

        /// <summary>
        ///     Decodes a base24-encoded string into a decimal
        /// </summary>
        /// <param name="value">Value to decode</param>
        /// <returns>Decoded decimal</returns>
        public static decimal? FromBase24String(string value)
        {
            return FromBase24String(value, null);
        }

        /// <summary>
        ///     Decodes a base24-encoded string into a decimal
        /// </summary>
        /// <param name="value">Value to decode</param>
        /// <param name="defaultValue">Value to return when decoding fails</param>
        /// <returns>Decoded decimal</returns>
        public static decimal? FromBase24String(string value, decimal? defaultValue)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            decimal? result = 0;
            foreach (char c in value.ToLower())
            {
                int index = Base24Digits.IndexOf(c);
                if (index < 0) return defaultValue; // not found means not base24

                result = (result*Base24Bits) + index;
            }
            return result;
        }
    }
}