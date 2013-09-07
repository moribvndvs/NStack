#region header

// <copyright file="FlakeType.cs" company="mikegrabski.com">
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
using System.Collections.Generic;
using System.Data;

using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace NStack.Data
{
    /// <summary>
    /// An implementation of <see cref="IUserType"/> for handling <see cref="Flake"/> values.
    /// </summary>
    public class FlakeType : IUserType, IParameterizedType
    {
        private byte _precision = 29;

        #region IUserType Members

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object value = NHibernateUtil.Decimal.NullSafeGet(rs, names);

            return value == null ? (object) null : (Flake) (decimal) value;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var parameter = (IDataParameter) cmd.Parameters[index];
            parameter.Value = value == null ? (object) DBNull.Value : (decimal) (Flake) value;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        public SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.GetSqlType(DbType.Decimal, _precision, 0)}; }
        }

        public Type ReturnedType
        {
            get { return typeof (Flake); }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IParameterizedType

        /// <summary>
        /// Gets called by Hibernate to pass the configured type parameters to 
        ///             the implementation.
        /// </summary>
        public void SetParameterValues(IDictionary<string, string> parameters)
        {
            if (parameters == null) return;

            var size = parameters.ContainsKey("flake-size") ? parameters["flake-size"] : "96";

            switch (size)
            {
                case "128":
                    _precision = 38;
                    break;
                case "96":
                    _precision = 29;
                    break;
                case "64":
                    _precision = 20;
                    break;
                default:
                    throw new MappingException("Unsupported flake size: " + size);


            }
        }

        #endregion
    }
}