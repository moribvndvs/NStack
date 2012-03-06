#region header

// <copyright file="StringExtensions.ConvertTo.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>

#endregion

using System;
using System.Globalization;

namespace MG.Extensions
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
        public static T ConvertTo<T>(string value, T defaultValue = default(T), IFormatProvider provider = null)
        {
            try
            {
                if (typeof(T) == typeof(Guid)) return (T) (object) (new Guid(value));

                return (T) Convert.ChangeType(value, typeof (T), provider ?? CultureInfo.CurrentCulture);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}