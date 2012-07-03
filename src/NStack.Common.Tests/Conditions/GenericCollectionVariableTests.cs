using System.Collections.Generic;

using NUnit.Framework;

namespace NStack.Conditions
{
    [TestFixture]
    public class GenericCollectionVariableTests : CollectionVariableTests<GenericCollectionVariable<string>, IEnumerable<string>, string>
    {
        #region Overrides of CollectionVariableTests<GenericCollectionVariable<string>,IEnumerable<string>,string,GenericCollectionVariable<string>>

        protected override void InitializeCollections()
        {
            Item = "Test";
            EmptyCollection = new GenericCollectionVariable<string>(new List<string>(), "EmptyCollection", false);
            NonEmptyCollection = new GenericCollectionVariable<string>(new List<string> {Item}, "NonEmptyCollection", false);
        }

        #endregion
    }
}