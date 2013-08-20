#region header

// <copyright file="GenericCollectionVariableTests.cs" company="mikegrabski.com">
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

using NUnit.Framework;

namespace NStack.Conditions
{
    [TestFixture]
    public class GenericCollectionVariableTests
        : CollectionVariableTests<GenericCollectionVariable<string>, IEnumerable<string>, string>
    {
        protected override void InitializeCollections()
        {
            Item = "Test";
            EmptyCollection = new GenericCollectionVariable<string>(new List<string>(), "EmptyCollection", false);
            NonEmptyCollection = new GenericCollectionVariable<string>(new List<string> {Item}, "NonEmptyCollection",
                                                                       false);
        }
    }
}