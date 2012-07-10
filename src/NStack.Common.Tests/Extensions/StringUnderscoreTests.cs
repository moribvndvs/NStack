#region header

// <copyright file="StringUnderscoreTests.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
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

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Extensions
{
    [TestFixture]
    public class StringUnderscoreTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        #endregion

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        [Test]
        public void Underscore_omits_punctuation()
        {
            // Arrange
            string original = "ThisIsASentence,AndAnother.123";

            // Act / Assert
            original.Underscore().Should().Be("this_is_a_sentence_and_another_123");
        }

        [Test]
        public void Underscore_underscores_before_numbers()
        {
            // Arrange
            string original = "ThisIsASentence234And2And23";

            // Act / Assert
            original.Underscore().Should().Be("this_is_a_sentence_234_and_2_and_23");
        }

        [Test]
        public void Underscore_underscores_changes_in_case()
        {
            // Arrange
            string original = "ThisIsASentence";

            // Act / Assert
            original.Underscore().Should().Be("this_is_a_sentence");
        }
    }
}