#region header

// <copyright file="InMemoryDatabase.cs" company="mikegrabski.com">
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

using Microsoft.Practices.ServiceLocation;

using NHibernate;
using NHibernate.Tool.hbm2ddl;

using NStack.Configuration;

namespace NStack.Testing
{
    /// <summary>
    /// A base type for managing an in-memory database for testing.
    /// </summary>
    /// <typeparam name="TDataAspect"></typeparam>
    public abstract class InMemoryDatabase<TDataAspect> : IDisposable
        where TDataAspect : NHDataAspect<TDataAspect>
    {
// ReSharper disable StaticFieldInGenericType
        private static readonly NHibernate.Cfg.Configuration Configuration;
// ReSharper restore StaticFieldInGenericType

// ReSharper disable StaticFieldInGenericType
        private static readonly ISessionFactory SessionFactory;
// ReSharper restore StaticFieldInGenericType

        static InMemoryDatabase()
        {
            Configuration = CreateConfiguration();
            SessionFactory = Configuration.BuildSessionFactory();
        }

        protected InMemoryDatabase()
        {
            Session = SessionFactory.OpenSession();

            var locator = new InMemoryServiceLocator();

            ServiceLocator.SetLocatorProvider(() => locator);

            BuildSchema(Session);
        }

        protected ISession Session { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            Session.Dispose();
        }

        #endregion

        /// <summary>
        /// Executes work in a transaction.
        /// </summary>
        /// <param name="work">A delegate containing the work to execute.</param>
        protected virtual void Tx(Action<ISession, ITransaction> work)
        {
            using (var tx = Session.BeginTransaction())
            {
                work(Session, tx);
                tx.Commit();
            }
        }

        /// <summary>
        /// Executes work in a transaction, then clears the session.
        /// </summary>
        /// <param name="work">A delegate containing the work to execute.</param>
        protected virtual void TxThenClear(Action<ISession, ITransaction> work)
        {
            Tx(work);
            Session.Clear();
        }

        private static NHibernate.Cfg.Configuration CreateConfiguration()
        {
            NHibernate.Cfg.Configuration config = null;

            Configure.Using(new NullContainerRegistry())
                     .Debugging()
                     .Testing()
                     .Aspect<TDataAspect>(aspect =>
                                          aspect.ExposeConfig(cfg => config = cfg));

            return config;
        }

        private static void BuildSchema(ISession session)
        {
            var export = new SchemaExport(Configuration);
            export.Execute(true, true, false, session.Connection, null);
        }
    }
}