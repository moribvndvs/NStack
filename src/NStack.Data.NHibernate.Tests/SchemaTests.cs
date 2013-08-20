#region header

// <copyright file="SchemaTests.cs" company="mikegrabski.com">
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

using System.Collections.Generic;
using System.Linq;

using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

namespace NStack.Data
{
    [TestFixture, Ignore]
    public class SchemaTests
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
        public void Script()
        {
            var automapper = new AutoMapper
                {
                    EntityBaseType = typeof (AutoMapperTestEntityBase)
                };
            automapper.MapAssemblyOf<SchemaTests>();

            IEnumerable<HbmMapping> compiled = automapper.Complete();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.ConnectionString = "Data Source=:memory:;Version=3;New=True";
                });

            configuration.AddDeserializedMapping(compiled.First(), string.Empty);

            var export = new SchemaExport(configuration);

            export.Execute(true, false, false);
        }
    }
}