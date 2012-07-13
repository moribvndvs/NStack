﻿

using System.Linq;

using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Data
{
    [TestFixture, Ignore]
    public class SchemaTests
    {
        #region Setup/Teardown for fixture

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        #endregion

        #region Setup/Teardown for each test

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
                EntityBaseType = typeof(AutoMapperTestEntityBase)
            };

            automapper.Override(mapping =>
            {
                mapping.Class<Parent>(m =>
                {
                    m.List(c => c.ListChildren, c => { });
                });
                mapping.Class<SingleTableBase>(m => m.Discriminator(d => d.Column("type")));
                mapping.Subclass<SingleTableA>(m => m.DiscriminatorValue("a"));
                mapping.Subclass<SingleTableB>(m => m.DiscriminatorValue("b"));
            });
            automapper.AddEntitiesFromAssemblyOf<SchemaTests>();

            var compiled = automapper.Complete();

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