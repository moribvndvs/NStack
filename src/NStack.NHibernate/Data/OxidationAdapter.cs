#region header

// <copyright file="OxidationAdapter.cs" company="mikegrabski.com">
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

using RustFlakes;

namespace NStack.Data
{
    /// <summary>
    /// Defines adapter classes for <see cref="Oxidation{T}"/>.
    /// </summary>
    public static class OxidationAdapter
    {
        public class Decimal : DecimalOxidation, IOxidation
        {
            private readonly uint _identifier;

            public Decimal(uint identifier)
                : base(identifier)
            {
                _identifier = identifier;
            }

            public Decimal(uint identifier, DateTime epoch) : base(identifier, epoch)
            {
                _identifier = identifier;
            }

            #region Implementation of IOxidation



            object IOxidation.Oxidize()
            {
                return Oxidize();
            }

            DateTime IOxidation.Epoch
            {
                get { return Epoch; }
            }

            object IOxidation.WorkerId
            {
                get { return _identifier; }
            }

            #endregion
        }

        public class UInt64 : UInt64Oxidation, IOxidation
        {
            public UInt64(ushort identifier) : base (identifier)
            {
                _identifier = identifier;
            }

            public UInt64(ushort identifier, DateTime epoch) : base(identifier, epoch)
            {
                _identifier = identifier;
            }

            private readonly ushort _identifier;

            #region Implementation of IOxidation

            object IOxidation.Oxidize()
            {
                return Oxidize();
            }

            DateTime IOxidation.Epoch
            {
                get { return Epoch; }
            }

            object IOxidation.WorkerId
            {
                get { return _identifier; }
            }

            #endregion
        }

        public class SqlBigInt : SqlServerBigIntOxidation, IOxidation
        {
            public SqlBigInt(ushort identifier) : base(identifier)
            {
                _identifier = identifier;
            }

            public SqlBigInt(ushort identifier, DateTime epoch) : base(identifier, epoch)
            {
                _identifier = identifier;
            }

            private readonly ushort _identifier;

            #region Implementation of IOxidation

            object IOxidation.Oxidize()
            {
                return Oxidize();
            }

            DateTime IOxidation.Epoch
            {
                get { return Epoch; }
            }

            object IOxidation.WorkerId
            {
                get { return _identifier; }
            }

            #endregion
        }

        public class BigInteger : BigIntegerOxidation, IOxidation
        {
            public BigInteger(ulong identifier) : base(identifier)
            {
                _identifier = identifier;
            }

            public BigInteger(ulong identifier, DateTime epoch) : base(identifier, epoch)
            {
                _identifier = identifier;
            }

            public BigInteger(byte[] identifier) : base(identifier)
            {
                _identifier = identifier;
            }

            public BigInteger(byte[] identifier, DateTime epoch) : base(identifier, epoch)
            {
                _identifier = identifier;
            }

            public BigInteger(byte[] identifier, DateTime epoch, bool littleEndian) : base(identifier, epoch, littleEndian)
            {
                _identifier = identifier;
            }

            private readonly object _identifier;

            #region Implementation of IOxidation

            object IOxidation.Oxidize()
            {
                return Oxidize();
            }

            DateTime IOxidation.Epoch
            {
                get { return Epoch; }
            }

            object IOxidation.WorkerId
            {
                get { return _identifier; }
            }

            #endregion
        }
    }
}