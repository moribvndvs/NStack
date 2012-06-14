#region header
// <copyright file="StringExtensions.Formatted.cs" company="mikegrabski.com">
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
using System.Globalization;

using NStack.Annotations;

namespace NStack.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Builds a string using the specified string the format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The format arguments.</param>
        /// <returns>The formatted string.</returns>
        [StringFormatMethod("format")]
        public static string Formatted(this string format, params object[] args)
        {
            return Formatted(format, null, args);
        }

        /// <summary>
        /// Builds a string using the specified string the format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="provider">The provider. </param>
        /// <param name="args">The format arguments.</param>
        /// <returns>The formatted string.</returns>
        [StringFormatMethod("format")]
        public static string Formatted(this string format, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider ?? CultureInfo.CurrentCulture, format, args);
        }
    }
}