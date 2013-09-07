#region header
// -----------------------------------------------------------------------
//  <copyright file="UnsupportedDateTimeOffsetType.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;
using System.Data;

using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace NStack.Data
{
    /// <summary>
    /// A user type that stores <see cref="DateTimeOffset"/> as regular <see cref="DateTime"/> (using the server's local timezone for the offset) for databases that don't support the type.
    /// </summary>
    [Serializable]
    public class UnsupportedDateTimeOffsetType : IUserType
    {
        [NonSerialized]
        private readonly TimeZoneInfo _databaseTimeZone = TimeZoneInfo.Local;

        #region IUserType Members

        public virtual Type ReturnedType
        {
            get { return typeof(DateTimeOffset); }
        }

        public virtual bool IsMutable
        {
            get { return false; }
        }

        public virtual object Disassemble(object value)
        {
            return value;
        }

        public virtual SqlType[] SqlTypes
        {
            get { return new[] { new SqlType(DbType.DateTime) }; }
        }

        public new virtual bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public virtual int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public virtual object NullSafeGet(IDataReader dr, string[] names,
                                          object owner)
        {
            object r = dr[names[0]];
            if (r == DBNull.Value)
            {
                return null;
            }

            var storedTime = (DateTime)r;
            return new DateTimeOffset(storedTime, _databaseTimeZone.BaseUtcOffset);
        }

        public virtual void NullSafeSet(IDbCommand cmd, object value, int
                                                                          index)
        {
            if (value == null)
            {
                NHibernateUtil.DateTime.NullSafeSet(cmd, null, index);
            }
            else
            {
                var parameter = (IDataParameter)cmd.Parameters[index];
                try
                {
                    var dateTimeOffset = (DateTimeOffset)value;
                    DateTime paramVal =
                        dateTimeOffset.ToOffset(_databaseTimeZone.BaseUtcOffset).DateTime;

                    parameter.Value = paramVal;
                }
                catch (Exception)
                {
                    parameter.Value = DateTime.MinValue;
                }
            }
        }

        public virtual object DeepCopy(object value)
        {
            return value;
        }

        public virtual object Replace(object original, object target, object
                                                                          owner)
        {
            return original;
        }

        public virtual object Assemble(object cached, object owner)
        {
            return cached;
        }

        #endregion
    }
}