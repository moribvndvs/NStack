#region header

// <copyright file="FlakeGenerator.cs" company="mikegrabski.com">
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
using System.Globalization;
using System.Numerics;

using Microsoft.Practices.ServiceLocation;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Engine;
using NHibernate.Id;
using NHibernate.Type;

using NStack.Extensions;

namespace NStack.Data
{
    /// <summary>
    ///     An implementation of <see cref="IIdentifierGenerator" /> for generating <see cref="Flake" /> values.
    /// </summary>
    public class FlakeGenerator : IIdentifierGenerator, IConfigurable
    {
        private const string EpochParamName = "epoch";

        private const string WorkerIdParamName = "worker-id";

        private const string OxidationTypeParamName = "oxidation-type";


        private static readonly IInternalLogger Log = LoggerProvider.LoggerFor(typeof (FlakeGenerator));

        private readonly Func<Type, IOxidation> _oxidationProvider;

        private static readonly Func<Type, IOxidation> DefaultOxidationProvider =
            type => (IOxidation) ServiceLocator.Current.GetInstance(type);

        /// <summary>
        /// Initializes a new instance of <see cref="FlakeGenerator"/>.
        /// </summary>
        public FlakeGenerator() : this(DefaultOxidationProvider)
        {
            
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public FlakeGenerator(Func<Type, IOxidation> oxidationProvider)
        {
            _oxidationProvider = oxidationProvider;
            Oxidation = () => null;
        }

        /// <summary>
        ///     Gets the delegate that returns the <see cref="IOxidation" /> implementation that has been configured.
        /// </summary>
        public Func<IOxidation> Oxidation { get; private set; }

        #region Implementation of IIdentifierGenerator

        /// <summary>
        ///     Generate a new identifier
        /// </summary>
        /// <param name="session">
        ///     The <see cref="T:NHibernate.Engine.ISessionImplementor" /> this id is being generated in.
        /// </param>
        /// <param name="obj">The entity for which the id is being generated.</param>
        /// <returns>
        ///     The new identifier
        /// </returns>
        public object Generate(ISessionImplementor session, object obj)
        {
            Ensures.That(Oxidation).IsNotNull("Oxidation has not been configured yet.");

            var oxidation = Oxidation();

            Ensures.That(oxidation).IsNotNull("Oxidation has not been configured yet.");

            var value = oxidation.Oxidize();

            if (value is BigInteger) return value;

            return (Flake) Convert.ToDecimal(value);

        }

        #endregion

        internal Func<IOxidation> CreateOxidationProvider<T>(DateTime? epoch, string workerIdRaw)
            where T : IOxidation
        {
            bool useCustom = epoch.HasValue || !string.IsNullOrEmpty(workerIdRaw);

            if (!useCustom) return () => _oxidationProvider(typeof (T));

            if (string.IsNullOrEmpty(workerIdRaw))
            {
                string msg = "If \"epoch\" is specified, \"worker-id\" must also be specified for FlakeGenerator.";
                Log.Error(msg);

                throw new MappingException(msg);
            }

            IOxidation instance;

            if (typeof (T) == typeof (OxidationAdapter.Decimal))
                instance = epoch.HasValue
                               ? new OxidationAdapter.Decimal(workerIdRaw.ConvertTo<uint>(), epoch.Value)
                               : new OxidationAdapter.Decimal(workerIdRaw.ConvertTo<uint>());

            else if (typeof (T) == typeof (OxidationAdapter.BigInteger))
            {
                var bytes = workerIdRaw.ConvertTo<byte[]>();

                if (bytes == null)
                {
                    var identifier = workerIdRaw.ConvertTo<ulong>();

                    instance = epoch.HasValue
                                   ? new OxidationAdapter.BigInteger(identifier, epoch.Value)
                                   : new OxidationAdapter.BigInteger(identifier);
                }
                else
                    instance = epoch.HasValue
                                   ? new OxidationAdapter.BigInteger(bytes, epoch.Value)
                                   : new OxidationAdapter.BigInteger(bytes);
            }
            else if (typeof (T) == typeof (OxidationAdapter.UInt64))
                instance = epoch.HasValue
                               ? new OxidationAdapter.UInt64(workerIdRaw.ConvertTo<ushort>(), epoch.Value)
                               : new OxidationAdapter.UInt64(workerIdRaw.ConvertTo<ushort>());
            else if (typeof (T) == typeof (OxidationAdapter.SqlBigInt))
                instance = epoch.HasValue
                               ? new OxidationAdapter.SqlBigInt(workerIdRaw.ConvertTo<ushort>(), epoch.Value)
                               : new OxidationAdapter.SqlBigInt(workerIdRaw.ConvertTo<ushort>());
            else throw new NotSupportedException("Unsupported oxidation adapter: " + typeof (T).FullName);

            return () => instance;
        }

        private static DateTime? GetEpoch(IDictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey(EpochParamName)) return null;

            string s = parameters[EpochParamName];
            DateTime value;

            return DateTime.TryParseExact(s, "u", CultureInfo.InvariantCulture,
                                          DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out value)
                       ? (DateTime?) value
                       : null;
        }

        #region Implementation of IConfigurable

        /// <summary>
        ///     Configure this instance, given the values of parameters
        ///     specified by the user as <c>&lt;param&gt;</c> elements.
        ///     This method is called just once, followed by instantiation.
        /// </summary>
        /// <param name="type">
        ///     The <see cref="T:NHibernate.Type.IType" /> the identifier should be.
        /// </param>
        /// <param name="parameters">
        ///     An <see cref="T:System.Collections.IDictionary" /> of Param values that are keyed by parameter name.
        /// </param>
        /// <param name="dialect">
        ///     The <see cref="T:NHibernate.Dialect.Dialect" /> to help with Configuration.
        /// </param>
        public void Configure(IType type, IDictionary<string, string> parameters, Dialect dialect)
        {
            DateTime? epoch = GetEpoch(parameters);
            string workerIdRaw = parameters.ContainsKey(WorkerIdParamName) ? parameters[WorkerIdParamName] : null;
            string oxidationType = parameters.ContainsKey(OxidationTypeParamName)
                                       ? parameters[OxidationTypeParamName]
                                       : "decimaloxidation";


            switch (oxidationType.ToLowerInvariant())
            {
                case "uint64oxidation":
                    Oxidation = CreateOxidationProvider<OxidationAdapter.UInt64>(epoch, workerIdRaw);
                    break;
                case "sqlserverbigintoxidation":
                    Oxidation = CreateOxidationProvider<OxidationAdapter.SqlBigInt>(epoch, workerIdRaw);
                    break;
                case "bigintegeroxidation":
                    Oxidation = CreateOxidationProvider<OxidationAdapter.BigInteger>(epoch, workerIdRaw);
                    break;
                case "":
                case "decimaloxidation":
                    Oxidation = CreateOxidationProvider<OxidationAdapter.Decimal>(epoch, workerIdRaw);
                    break;
                default:
                    string msg = "The \"oxidation-type\" parameter for FlakeGenerator is unsupported: " + oxidationType;
                    Log.Error(msg);
                    throw new MappingException(msg);
            }
        }

        #endregion
    }
}