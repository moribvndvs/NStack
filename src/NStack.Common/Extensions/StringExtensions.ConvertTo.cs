#region header
// <copyright file="StringExtensions.ConvertTo.cs" company="mikegrabski.com">
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NStack.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Converts the specified string to the specified type.
        /// </summary>
        /// <typeparam name="T">Type the string should be converted to.</typeparam>
        /// <param name="value">The value to be converted.</param>
        /// <param name="defaultValue">The default value that should be returned if conversion fails.</param>
        /// <param name="provider">The format provider.</param>
        /// <returns>The value of the string converted to the desired type, or the default value if conversion failed.</returns>
        public static T ConvertTo<T>(this string value, T defaultValue = default(T), IFormatProvider provider = null)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            try
            {
                if (typeof(T) == typeof(Guid)) return (T) (object) (new Guid(value));
                if (typeof(T).IsEnum) return (T) Enum.Parse(typeof (T), value, true);
                if (typeof(T) == typeof(byte[]))
                    return (T) (object) ConvertToByteArray(value, (byte[]) (object) defaultValue, provider);

                return (T) Convert.ChangeType(value, typeof (T), provider ?? CultureInfo.CurrentCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static byte[] ConvertToByteArray(string value, byte[] defaultValue, IFormatProvider provider)
        {
            if (IsPossiblyHexEncoded(value)) return ConvertHexEncodedString(value, defaultValue, provider);
            if (IsPossiblyBase64Encoded(value)) return ConvertBase64EncodedString(value, defaultValue, provider);

            return defaultValue;
        }

        private static byte[] ConvertHexEncodedString(string value, byte[] defaultValue, IFormatProvider provider)
        {
            var list = new List<byte>();

            for (int i = 0; i < value.Length; i = i+2)
            {
                byte b;
                var hex = value.Substring(i, 2);

                if (!byte.TryParse(hex, NumberStyles.HexNumber, provider, out b)) return defaultValue;

                list.Add(b);
            }

            return list.ToArray();
        }

        private static byte[] ConvertBase64EncodedString(string value, byte[] defaultValue, IFormatProvider provider)
        {
            try
            {
                return Convert.FromBase64String(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static readonly HashSet<char> Base64Characters = new HashSet<char> { 
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 
                'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
                'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', 
                '='
            };

        private static readonly HashSet<char> HexCharacters = new HashSet<char>
                                                                  {
                                                                      'A', 'B', 'C', 'D', 'E', 'F',
                                                                      '0', '1', '2', '3', '4', '5',
                                                                      '6', '7', '8', '9', 'a', 'b',
                                                                      'c', 'd', 'e', 'f'
                                                                  }; 

        private static bool IsPossiblyBase64Encoded(string value)
        {
            return !string.IsNullOrEmpty(value) &&  value.All(c => Base64Characters.Contains(c));
        }

        private static bool IsPossiblyHexEncoded(string value)
        {
            return !string.IsNullOrEmpty(value) 
                && value.Length%2 == 0 
                && value.All(c => HexCharacters.Contains(c));
        }
    }
}