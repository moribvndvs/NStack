#region header

// <copyright file="FlakeGeneratorTests.cs" company="mikegrabski.com">
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
using System.Numerics;

using FluentAssertions;

using NHibernate;
using NHibernate.Dialect;
using NHibernate.Type;

using NStack.Extensions;

using NUnit.Framework;

using RustFlakes;

namespace NStack.Data
{
    [TestFixture]
    public class FlakeGeneratorTests
    {
        #region Setup/Teardown

        [TestFixtureSetUp]
        public void InitFixture()
        {
            _oxidationProvider = type => _oxidizers[type];
        }

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [TestFixtureTearDown]
        public void CleanupFixture()
        {
        }

        #endregion

        private const ulong VeryLongIdentifier = 0x1234567890;

        private const uint LongIdentifier = 0x12345678;

        private const ushort ShortIdentifier = 1;
        private const string OxidationTypeParamKey = "oxidation_type";
        private const string WorkerIdParamKey = "worker_id";
        private const string EpochParamKey = "epoch";

        private readonly IType _flakeType = new CustomType(typeof (FlakeType), new Dictionary<string, string>());

        private readonly IDictionary<string, string> _empty = new Dictionary<string, string>(0);

        private Func<Type, IOxidation> _oxidationProvider;

        private readonly Dictionary<Type, string> _workerIds = new Dictionary<Type, string>
            {
                {typeof (DecimalOxidation), "1"},
                {typeof (UInt64Oxidation), "2"},
                {typeof (SqlServerBigIntOxidation), "3"},
                {typeof (BigIntegerOxidation), new byte[] {0, 1, 2, 3, 4, 5}.ToHexString()}
            };

        private readonly Dictionary<Type, Type> _oxidizerAdapterMap = new Dictionary<Type, Type>
            {
                {typeof (DecimalOxidation), typeof (OxidationAdapter.Decimal)},
                {typeof (UInt64Oxidation), typeof (OxidationAdapter.UInt64)},
                {typeof (SqlServerBigIntOxidation), typeof (OxidationAdapter.SqlBigInt)},
                {typeof (BigIntegerOxidation), typeof (OxidationAdapter.BigInteger)},
            };

        private readonly Dictionary<Type, IOxidation> _oxidizers = new Dictionary<Type, IOxidation>
            {
                {typeof (OxidationAdapter.Decimal), new OxidationAdapter.Decimal(LongIdentifier)},
                {typeof (OxidationAdapter.UInt64), new OxidationAdapter.UInt64(ShortIdentifier)},
                {typeof (OxidationAdapter.SqlBigInt), new OxidationAdapter.SqlBigInt(ShortIdentifier)},
                {typeof (OxidationAdapter.BigInteger), new OxidationAdapter.BigInteger(VeryLongIdentifier)}
            };

        

        [TestCase(typeof(DecimalOxidation))]
        [TestCase(typeof(UInt64Oxidation))]
        [TestCase(typeof(SqlServerBigIntOxidation))]
        [TestCase(typeof(BigIntegerOxidation))]
        public void Configure_should_use_oxidation_from_provider(Type oxidationType)
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            // Act
            generator.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, oxidationType.Name}
                }, new GenericDialect());

            IOxidation oxidation = generator.Oxidation();

            // Assert
            oxidation.Should().Be(_oxidizers[_oxidizerAdapterMap[oxidationType]]);
        }

        [TestCase(typeof(DecimalOxidation))]
        [TestCase(typeof(UInt64Oxidation))]
        [TestCase(typeof(SqlServerBigIntOxidation))]
        [TestCase(typeof(BigIntegerOxidation))]
        public void Configure_should_use_specific_oxidation_and_worker_id_and_epoch(Type oxidationType)
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);
            var epoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act
            generator.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, oxidationType.Name},
                    {WorkerIdParamKey, _workerIds[oxidationType]},
                    {EpochParamKey, epoch.ToString("u")}
                }, new GenericDialect());

            IOxidation oxidation = generator.Oxidation();

            // Assert
            oxidation.GetType().Should().Be(_oxidizerAdapterMap[oxidationType]);
            oxidation.Epoch.Should().Be(epoch);

            if (oxidationType == typeof (BigIntegerOxidation))
                oxidation.WorkerId.As<byte[]>().Should().ContainInOrder(_workerIds[oxidationType].ConvertTo<byte[]>());
            else Convert.ToUInt64(oxidation.WorkerId).Should().Be(_workerIds[oxidationType].ConvertTo<ulong>());
        }
        
        
        [TestCase(typeof(DecimalOxidation))]
        [TestCase(typeof(UInt64Oxidation))]
        [TestCase(typeof(SqlServerBigIntOxidation))]
        [TestCase(typeof(BigIntegerOxidation))]
        public void Configure_should_throw_if_epoch_present_but_worker_id_is_missing(Type oxidationType)
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);
            var epoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act / Assert
            generator.Invoking(g => g.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, oxidationType.Name},
                    {EpochParamKey, epoch.ToString("u")}
                }, new GenericDialect()))
                     .ShouldThrow<MappingException>();
        }

        [Test]
        public void Configure_should_default_to_decimal_oxidation_from_provider()
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            // Act
            generator.Configure(_flakeType, _empty, new GenericDialect());

            var oxidation = generator.Oxidation();

            // Assert
            oxidation.Should().Be(_oxidizers[_oxidizerAdapterMap[typeof(DecimalOxidation)]]);

        }

        [Test]
        public void Configure_should_throw_on_invalid_oxidation_type()
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            // Act / Assert

            generator.Invoking(g => generator.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, "unknown"}
                }, new GenericDialect()))
                     .ShouldThrow<MappingException>();

        }

        [TestCase(typeof(DecimalOxidation))]
        [TestCase(typeof(UInt64Oxidation))]
        [TestCase(typeof(SqlServerBigIntOxidation))]
        public void Generate_should_return_flake_values(Type oxidationType)
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            generator.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, oxidationType.Name}
                }, new GenericDialect());

            // Act
            var actual = generator.Generate(null, null);

            // Assert
            actual.Should().BeOfType<Flake>();
            actual.As<Flake>().Should().BeGreaterThan(Flake.Unassigned);
        }

        [Test]
        public void Generate_should_return_BigInteger()
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            generator.Configure(_flakeType, new Dictionary<string, string>
                {
                    {OxidationTypeParamKey, "bigintegeroxidation"}
                }, new GenericDialect());

            // Act
            var actual = generator.Generate(null, null);

            // Assert
            actual.Should().BeOfType<BigInteger>();
            actual.As<BigInteger>().Should().BeGreaterThan(BigInteger.Zero);
        }

        [Test]
        public void Generate_should_throw_if_no_oxidation_configured()
        {
            // Arrange
            var generator = new FlakeGenerator(_oxidationProvider);

            // Act / Assert
            generator.Invoking(g => g.Generate(null, null))
                     .ShouldThrow<Conditions.PostConditionException>();
        }
    }
}