#region header

// <copyright file="FlakeConverter.cs" company="mikegrabski.com">
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
using System.ComponentModel;
using System.Globalization;

namespace NStack.ComponentModel
{
    /// <summary>
    ///     A <see cref="TypeConverter" /> for converting <see cref="Flake" />.
    /// </summary>
    public class FlakeConverter : TypeConverter
    {
        /// <summary>
        ///     Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <returns>
        ///     true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        /// <param name="context">
        ///     An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.
        /// </param>
        /// <param name="sourceType">
        ///     A <see cref="T:System.Type" /> that represents the type you want to convert from.
        /// </param>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof (string) || sourceType == typeof (decimal)) return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        ///     Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        /// <param name="context">
        ///     An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.
        /// </param>
        /// <param name="culture">
        ///     The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.
        /// </param>
        /// <param name="value">
        ///     The <see cref="T:System.Object" /> to convert.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is decimal) return new Flake((decimal) value);

            var s = value as string;
            if (s != null)
            {
                return Flake.Parse(s);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        ///     Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        /// <param name="context">
        ///     An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.
        /// </param>
        /// <param name="culture">
        ///     A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.
        /// </param>
        /// <param name="value">
        ///     The <see cref="T:System.Object" /> to convert.
        /// </param>
        /// <param name="destinationType">
        ///     The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="destinationType" /> parameter is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            if (destinationType == typeof (string) && value is Flake)
            {
                return ((Flake) value).ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}