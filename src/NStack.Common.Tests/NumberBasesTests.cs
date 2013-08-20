#region header

// <copyright file="NumberBasesTests.cs" company="mikegrabski.com">
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

using NUnit.Framework;

namespace NStack
{
    [TestFixture]
    public class NumberBasesTests
    {
        #region Setup/Teardown

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
        public void FromBase24String_should_convert_to_decimal()
        {
            // decimal version
            decimal counter = 1;
            string expected = "C";
            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(counter, NumberBases.FromBase24String(expected));
                if (counter < decimal.MaxValue/24m)
                {
                    counter *= 24m;
                    expected += "B";
                }
            }
        }

        [Test]
        public void ToBase24String_should_convert_to_string()
        {
            decimal counter = 1;
            string expected = "c";
            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(expected, NumberBases.ToBase24String(counter));

                if (counter < decimal.MaxValue/24m)
                {
                    counter *= 24m;
                    expected += "b";
                }
            }
        }
    }
}