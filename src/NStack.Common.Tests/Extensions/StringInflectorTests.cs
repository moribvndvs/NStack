

using NUnit.Framework;

using FluentAssertions;

using Moq;

namespace NStack.Extensions
{
    [TestFixture]
    public class StringInflectorTests
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
        public void Pluralize_returns_plural_form_for_known_word_patterns()
        {
            "clown".Pluralize().Should().Be("clowns");
            "clown".Pluralize().Should().Be("clowns");
            "blow".Pluralize().Should().Be("blows");
            "axis".Pluralize().Should().Be("axes");
            "testis".Pluralize().Should().Be("testes");
            "octopus".Pluralize().Should().Be("octopi");
            "virus".Pluralize().Should().Be("viri");
            "alumnus".Pluralize().Should().Be("alumni");
            "fungus".Pluralize().Should().Be("fungi");
            "alias".Pluralize().Should().Be("aliases");
            "status".Pluralize().Should().Be("statuses");
            "buffalo".Pluralize().Should().Be("buffaloes");
            "tomato".Pluralize().Should().Be("tomatoes");
            "volcano".Pluralize().Should().Be("volcanoes");
            "diagnosis".Pluralize().Should().Be("diagnoses");
            "half".Pluralize().Should().Be("halves");
            "bay".Pluralize().Should().Be("bays");
            "fly".Pluralize().Should().Be("flies");
            "hive".Pluralize().Should().Be("hives");
            "box".Pluralize().Should().Be("boxes");
            "truss".Pluralize().Should().Be("trusses");
            "church".Pluralize().Should().Be("churches");
            "wash".Pluralize().Should().Be("washes");
            "matrix".Pluralize().Should().Be("matrices");
            "vertex".Pluralize().Should().Be("vertices");
            "index".Pluralize().Should().Be("indices");
            "mouse".Pluralize().Should().Be("mice");
            "louse".Pluralize().Should().Be("lice");
            "ox".Pluralize().Should().Be("oxen");
            "quiz".Pluralize().Should().Be("quizzes");
            "person".Pluralize().Should().Be("people");
            "man".Pluralize().Should().Be("men");
            "woman".Pluralize().Should().Be("women");
            "child".Pluralize().Should().Be("children");
            "sex".Pluralize().Should().Be("sexes");
            "move".Pluralize().Should().Be("moves");
            "goose".Pluralize().Should().Be("geese");
            "moose".Pluralize().Should().Be("moose");
            "alumna".Pluralize().Should().Be("alumnae");
            "equipment".Pluralize().Should().Be("equipment");
            "information".Pluralize().Should().Be("information");
            "rice".Pluralize().Should().Be("rice");
            "money".Pluralize().Should().Be("money");
            "species".Pluralize().Should().Be("species");
            "series".Pluralize().Should().Be("series");
            "sheep".Pluralize().Should().Be("sheep");
            "deer".Pluralize().Should().Be("deer");
            "aircraft".Pluralize().Should().Be("aircraft");
        }

        [Test]
        public void Singularize_singularizes_known_word_patterns()
        {
           "clowns".Singularize().Should().Be("clown");
           "blows".Singularize().Should().Be("blow");
           "axes".Singularize().Should().Be("axis");
           "testes".Singularize().Should().Be("testis");
           "octopi".Singularize().Should().Be("octopus");
           "viri".Singularize().Should().Be("virus");
           "alumni".Singularize().Should().Be("alumnus");
           "fungi".Singularize().Should().Be("fungus");
           "aliases".Singularize().Should().Be("alias");
           "statuses".Singularize().Should().Be("status");
           "buffaloes".Singularize().Should().Be("buffalo");
           "tomatoes".Singularize().Should().Be("tomato");
           "diagnoses".Singularize().Should().Be("diagnosis");
           "halves".Singularize().Should().Be("half");
           "bays".Singularize().Should().Be("bay");
           "flies".Singularize().Should().Be("fly");
           "hives".Singularize().Should().Be("hive");
           "boxes".Singularize().Should().Be("box");
           "trusses".Singularize().Should().Be("truss");
           "churches".Singularize().Should().Be("church");
           "washes".Singularize().Should().Be("wash");
           "matrices".Singularize().Should().Be("matrix");
           "vertices".Singularize().Should().Be("vertex");
           "indices".Singularize().Should().Be("index");
           "mice".Singularize().Should().Be("mouse");
           "lice".Singularize().Should().Be("louse");
           "oxen".Singularize().Should().Be("ox");
           "quizzes".Singularize().Should().Be("quiz");
           "people".Singularize().Should().Be("person");
           "men".Singularize().Should().Be("man");
           "women".Singularize().Should().Be("woman");
           "children".Singularize().Should().Be("child");
           "sexes".Singularize().Should().Be("sex");
           "moves".Singularize().Should().Be("move");
           "geese".Singularize().Should().Be("goose");
           "moose".Singularize().Should().Be("moose");
           "alumnae".Singularize().Should().Be("alumna");
           "equipment".Singularize().Should().Be("equipment");
           "information".Singularize().Should().Be("information");
           "rice".Singularize().Should().Be("rice");
           "money".Singularize().Should().Be("money");
           "species".Singularize().Should().Be("species");
           "sheep".Singularize().Should().Be("sheep");
           "series".Singularize().Should().Be("series");
           "deer".Singularize().Should().Be("deer");
           "aircraft".Singularize().Should().Be("aircraft");
        }
    }
}