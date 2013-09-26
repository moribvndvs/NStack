#region header

// <copyright file="ByteArray.ToHex.cs" company="mikegrabski.com">
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

using System.Text;

namespace NStack.Extensions
{
    public static partial class ByteArray
    {
        /// <summary>
        ///     Returns the contents of the byte array as a string of hex equivalents.
        /// </summary>
        /// <param name="array"> The array. </param>
        /// <returns> A string of hex values representing the original array. </returns>
        public static string ToHexString(this byte[] array)
        {
            var result = new StringBuilder();

            foreach (byte b in array)
            {
                result.AppendFormat("{0:x2}", b);
            }

            return result.ToString();
        }
    }
}