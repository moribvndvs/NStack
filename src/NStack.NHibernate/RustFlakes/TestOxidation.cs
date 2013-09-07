#region header
// -----------------------------------------------------------------------
//  <copyright file="TestOxidation.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using System;

namespace RustFlakes
{
    public class TestOxidation : Oxidation<ulong>
    {
        public TestOxidation()
            : this(new DateTime(2013, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
        {
        }

        public TestOxidation(DateTime epoch) : base(epoch)
        {
        }

        #region Overrides of Oxidation<long>

        public override ulong Oxidize()
        {
            Update();
            return (((uint) LastOxidizedInMs) << 32) + (0 << 16) + Counter;
        }

        #endregion
    }
}