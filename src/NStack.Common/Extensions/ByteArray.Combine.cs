#region header
// -----------------------------------------------------------------------
//  <copyright file="ByteArray.Combine.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.Linq;

namespace NStack.Extensions
{
    public static partial class ByteArray
    {
        /// <summary>
        /// Combines the contents of the first and all additional byte arrays into one new array.
        /// </summary>
        /// <param name="first">The first array.</param>
        /// <param name="additional">Additional arrays.</param>
        /// <returns>A byte array containing all of the bytes combined in order.</returns>
        public static byte[] Combine(this byte[] first, params byte[][] additional)
        {
            Requires.That(first, "first").IsNotNull();
            Requires.That(additional, "additional").IsNotNull();

            var arrays = new byte[additional.Length + 1][];
            arrays[0] = first;

            Array.Copy(additional, 0, arrays, 1, additional.Length);

            var result = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;

            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }
    }
}