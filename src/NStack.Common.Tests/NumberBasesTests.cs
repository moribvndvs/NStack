#region header
// -----------------------------------------------------------------------
//  <copyright file="NumberBasesTests.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using NUnit.Framework;

namespace NStack
{
    [TestFixture]
    public class NumberBasesTests
    {
        #region Set-up and Tear-down

        [TestFixtureSetUp]
        public void SetUpFixture()
        {

        }

        [SetUp]
        public void SetUpTest()
        {

        }

        [TearDown]
        public void TearDownTest()
        {

        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {

        }

        #endregion

        [Test]
        public void ToBase24String_should_convert_to_string()
        {
            decimal counter = 1;
            var expected = "c";
            for (var i = 0; i < 21; i++)
            {
                Assert.AreEqual(expected, NumberBases.ToBase24String(counter));

                if (counter < decimal.MaxValue / 24m)
                {
                    counter *= 24m;
                    expected += "b";
                }
            }
        }

        [Test]
        public void FromBase24String_should_convert_to_decimal()
        {
            // decimal version
            decimal counter = 1;
            var expected = "C";
            for (var i = 0; i < 21; i++)
            {
                Assert.AreEqual(counter, NumberBases.FromBase24String(expected));
                if (counter < decimal.MaxValue / 24m)
                {
                    counter *= 24m;
                    expected += "B";
                }
            }
        }
    }
}