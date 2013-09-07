#region header
// <copyright file="NHDataAspect.cs" company="mikegrabski.com">
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
using System.Configuration;
using System.IO;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Tool.hbm2ddl;

using NStack.Data;

using NStack.Extensions;

namespace NStack.Configuration
{
    /// <summary>
    /// A concrete implementation of <see cref="NHDataAspect{TThis}"/> for consumers that do not wish to define their own derivative data aspect.
    /// </summary>
    public class NHDataAspect : NHDataAspect<NHDataAspect>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public NHDataAspect(IConfiguration configuration) : base(configuration)
        {
        }
    }

    /// <summary>
    /// A base type for <see cref="IDataAspect"/> that uses NHibernate.
    /// </summary>
    /// <typeparam name="TThis">The top-most implementing type.</typeparam>
    public abstract class NHDataAspect<TThis> : ConfigurationAspect, IDataAspect
            where TThis : NHDataAspect<TThis>
    {
        private bool _prepared;

        private readonly Dictionary<Type, Func<IConfiguration, object[]>> _oxidationParams =
            new Dictionary<Type, Func<IConfiguration, object[]>>(); 

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected NHDataAspect(IConfiguration configuration) : base(configuration)
        {
            
            Config = new NHibernate.Cfg.Configuration();
        }

        /// <summary>
        /// Gets the NHibernate configuration..
        /// </summary>
        protected internal NHibernate.Cfg.Configuration Config { get; private set; }

        /// <summary>
        /// Configures the default schema.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The aspect configuration.</returns>
        public TThis DefaultSchema(string value)
        {
            Config.SessionFactory().Mapping.UsingDefaultSchema(value);

            return (TThis) this;
        }

        /// <summary>
        /// Configures the database properties used by NHibernate. 
        /// </summary>
        /// <param name="configure">Action that is invoked to configure the database properties.</param>
        /// <returns>The aspect configuration.</returns>
        public TThis Database(Action<IDbIntegrationConfigurationProperties> configure)
        {
            Config.DataBaseIntegration(configure);

            return (TThis) this;
        }

        /// <summary>
        /// Exposes the NH configuration to a delegate so it can be further configured or examined.
        /// </summary>
        /// <param name="config">Delegate that is invoked with the current NH config.</param>
        /// <returns>The aspect configuration.</returns>
        public TThis ExposeConfig(Action<NHibernate.Cfg.Configuration> config)
        {
            Requires.That(config).IsNotNull();

            config(Config);

            return (TThis) this;
        }

        /// <summary>
        /// Configures the data aspect to use the specified <see cref="Oxidation{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolveParameters"></param>
        /// <returns></returns>
        public TThis Oxidation<T>(Func<IConfiguration, object[]> resolveParameters)
            where T : IOxidation

        {
            _oxidationParams.Add(typeof(T), resolveParameters);

            return (TThis) this;
        }

        /// <summary>
        /// Exports the database schema.
        /// </summary>
        /// <param name="export">The delegate invoked to handle the export process.</param>
        /// <returns></returns>
        public TThis ExportSchema(Action<SchemaExport> export)
        {
            Requires.That(export).IsNotNull();

            EnsurePrepared();

            var exporter = new SchemaExport(Config);

            export(exporter);

            return (TThis) this;
        }

        /// <summary>
        /// Updates the database schema.
        /// </summary>
        /// <param name="update">The delegate invoked to handle the update process.</param>
        /// <returns></returns>
        public TThis UpdateSchema(Action<SchemaUpdate> update)
        {
            Requires.That(update).IsNotNull();

            EnsurePrepared();

            var updater = new SchemaUpdate(Config);

            update(updater);

            return (TThis) this;
        }

        protected internal void RegisterOxidations(IContainerRegistry registry)
        {
            foreach (var type in _oxidationParams.Keys)
            {
                var parameters = _oxidationParams[type](Configuration);
                var oxidation = (IOxidation) Activator.CreateInstance(type, parameters);

                registry.RegisterSingleInstance(type, oxidation);
            }
        }

        protected internal void RegisterNHibernate(IContainerRegistry registry)
        {
            var sessionFactory = Config.BuildSessionFactory();

            registry.RegisterSingleInstance<ISessionFactory, ISessionFactory>(sessionFactory);
            registry.Register<ISession, ISession>(resolver => resolver.Get<ISessionFactory>().OpenSession());
            registry.Register<IStatelessSession, IStatelessSession>(
                resolver => resolver.Get<ISessionFactory>().OpenStatelessSession());
            registry.Register<NHUnitOfWork, NHUnitOfWork>();
            registry.Register<IUnitOfWork, IUnitOfWork>(resolver => resolver.Get<NHUnitOfWork>());
            registry.RegisterGeneric(typeof(IRepository<>), typeof(NHRepository<>));
            registry.RegisterGeneric(typeof(IRepository<,>), typeof(NHRepository<,>));
        }

        #region Overrides of ConfigurationAspect

        /// <summary>
        ///     Builds the configuration for the aspect.
        /// </summary>
        protected override void Configure()
        {
            EnsurePrepared();
            DoSchemaInit();
        }

        private static bool HasSchemaActionFlag(SchemaAction action, SchemaAction flag)
        {
            return (action & flag) != 0;
        }

        private void DoSchemaInit()
        {
            var action =
                ConfigurationManager.AppSettings["NStack.Data:SchemaAction"].ConvertTo<SchemaAction>();
            var scriptPath = ConfigurationManager.AppSettings["NStack.Data:SchemaScriptPath"];
            var script = action.HasFlag(SchemaAction.Script);
            var execute = action.HasFlag(SchemaAction.Execute);

            if (!script && !execute) return; // nothing to do
            if (action.HasFlag(SchemaAction.Verify)) throw new NotImplementedException("Schema Verification is not implemented yet.");
            if (script && string.IsNullOrEmpty(scriptPath))
                throw new InvalidOperationException(
                    "A script path must be specified by adding the key NStack.Data:SchemaScriptPath.");

            if (action.HasFlag(SchemaAction.Drop | SchemaAction.Create))
            {
                ExportSchema(export =>
                    {
                        if (script) export.SetOutputFile(scriptPath);
                        export.Execute(script, execute, false);
                    });
            }
            else if (action.HasFlag(SchemaAction.Drop))
            {
                ExportSchema(export =>
                    {
                        if (script) export.SetOutputFile(scriptPath);
                        export.Drop(script, execute);
                    });
            }
            else if (action.HasFlag(SchemaAction.Create))
            {
                ExportSchema(export =>
                    {
                        if (script) export.SetOutputFile(scriptPath);
                        export.Create(script, execute);
                    });
            }
            else if (action.HasFlag(SchemaAction.Update))
            {
                UpdateSchema(update =>
                    {
                        if (script)
                        {
                            using (var file = new StreamWriter(scriptPath))
                            {
                                update.Execute(file.WriteLine, execute);
                            }
                        }
                        else update.Execute(false, execute);
                    });
            }
        }

        #endregion

        protected internal void EnsurePrepared()
        {
            if (!_prepared)
            {
                PrepareConfiguration();
                _prepared = true;
            }
        }

        protected internal virtual void PrepareConfiguration()
        {
            
        }

        protected override void ConfigureContainer(IContainerRegistry registry)
        {
            RegisterNHibernate(registry);
            RegisterOxidations(registry);
        }

    }
}