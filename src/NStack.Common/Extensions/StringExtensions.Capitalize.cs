#region header

// <copyright file="StringExtensions.Capitalize.cs" company="mikegrabski.com">
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

using System.Text;

namespace NStack.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///   Capitalizes the first letter in the string.
        /// </summary>
        /// <param name="original"> The original string. </param>
        /// <returns> The string, all lower case except for the first letter. </returns>
        public static string Capitalize(this string original)
        {
            if (string.IsNullOrEmpty(original)) return original;

            var buffer = new StringBuilder(original.ToLower());

            for (int i = 0; i < buffer.Length; i++)
            {
                char c = buffer[i];

                if (!char.IsLetter(c)) continue;

                buffer[i] = char.ToUpper(c);
                break;
            }

            return buffer.ToString();
        }
    }
}