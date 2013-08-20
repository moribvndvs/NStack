#region header

// <copyright file="StringConvertToTests.cs" company="mikegrabski.com">
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
using System.Globalization;

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Extensions
{
    [TestFixture]
    public class StringConvertToTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
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

        #endregion

        [Test]
        public void Should_convert_booleans()
        {
            "false".ConvertTo(true).Should().BeFalse();
            "FALSE".ConvertTo(true).Should().BeFalse();
            "False".ConvertTo(true).Should().BeFalse();
            "true".ConvertTo<bool>().Should().BeTrue();
            "TRUE".ConvertTo<bool>().Should().BeTrue();
            "True".ConvertTo<bool>().Should().BeTrue();
        }

        [Test]
        public void Should_convert_byte_array()
        {
            // Arrange
            var expected = new byte[] {10, 24, 100, 23, 93, 45, 200};

            // Act / Assert
            Convert.ToBase64String(expected).ConvertTo<byte[]>().Should().Equal(expected);
            expected.ToHexString().ConvertTo<byte[]>().Should().Equal(expected);
        }

        [Test]
        public void Should_convert_datetimes()
        {
            "6/10/2012 1:15 PM".ConvertTo<DateTime>(provider: CultureInfo.InvariantCulture)
                               .Should().Be(new DateTime(2012, 6, 10, 13, 15, 0));
            "6/10/2012".ConvertTo<DateTime>(provider: CultureInfo.InvariantCulture)
                       .Should().Be(new DateTime(2012, 6, 10));
        }

        [Test]
        public void Should_convert_enum_names()
        {
            "sunday".ConvertTo(DayOfWeek.Tuesday).Should().Be(DayOfWeek.Sunday);
            "Sunday".ConvertTo(DayOfWeek.Tuesday).Should().Be(DayOfWeek.Sunday);
            "SUNDAY".ConvertTo(DayOfWeek.Tuesday).Should().Be(DayOfWeek.Sunday);
        }

        [Test]
        public void Should_convert_guid()
        {
            // Arrange
            Guid guid = Guid.NewGuid();

            // Act / Assert
            guid.ToString().ConvertTo<Guid>().Should().Be(guid);
            guid.ToString("N").ConvertTo<Guid>().Should().Be(guid);
            guid.ToString("B").ConvertTo<Guid>().Should().Be(guid);
            guid.ToString("P").ConvertTo<Guid>().Should().Be(guid);
            guid.ToString("X").ConvertTo<Guid>().Should().Be(guid);
        }

        [Test]
        public void Should_convert_numeric_types()
        {
            byte.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<byte>().Should().Be(byte.MaxValue);
            short.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<short>().Should().Be(short.MaxValue);
            ushort.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<ushort>().Should().Be(ushort.MaxValue);
            int.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<int>().Should().Be(int.MaxValue);
            uint.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<uint>().Should().Be(uint.MaxValue);
            long.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<long>().Should().Be(long.MaxValue);
            ulong.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<ulong>().Should().Be(ulong.MaxValue);
            decimal.MaxValue.ToString(CultureInfo.InvariantCulture).ConvertTo<decimal>().Should().Be(decimal.MaxValue);
        }
    }
}