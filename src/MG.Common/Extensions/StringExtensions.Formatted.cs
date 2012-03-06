#region header
// <copyright file="StringExtensions.Formatted.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>
#endregion

using System;
using System.Globalization;

using MG.Annotations;

namespace MG.Extensions
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