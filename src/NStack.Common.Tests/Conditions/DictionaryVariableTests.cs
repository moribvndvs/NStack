#region header

// <copyright file="DictionaryVariableTests.cs" company="mikegrabski.com">
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
using System.Collections;

using FluentAssertions;

using NUnit.Framework;

namespace NStack.Conditions
{
    public abstract class DictionaryVariableTests<T, TVariable, TKey, TValue, TPair>
        : CollectionVariableTests<T, TVariable, TPair>
        where TVariable : IEnumerable
        where T : DictionaryVariable<TVariable, TKey, TValue, TPair, T>
    {
        protected abstract TKey GetItemKey();

        protected abstract TValue GetItemValue();

        [Test]
        public void ContainsKey_should_fail()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.ContainsKey(GetItemKey()))
                           .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ContainsKey_should_pass()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.ContainsKey(GetItemKey()))
                              .ShouldNotThrow();
        }

        [Test]
        public void ContainsValue_should_fail()
        {
            NonEmptyCollection.Invoking(value => value.ContainsValue(GetItemValue()))
                              .ShouldNotThrow();
        }

        [Test]
        public void ContainsValue_should_pass()
        {
            NonEmptyCollection.Invoking(value => value.ContainsValue(GetItemValue()))
                              .ShouldNotThrow();
        }

        [Test]
        public void DoesNotContainKey_should_fail()
        {
            // Act / Assert
            NonEmptyCollection.Invoking(value => value.DoesNotContainKey(GetItemKey()))
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DoesNotContainKey_should_pass()
        {
            // Act / Assert
            EmptyCollection.Invoking(value => value.DoesNotContainKey(GetItemKey()))
                           .ShouldNotThrow();
        }

        [Test]
        public void DoesNotContainValue_should_fail()
        {
            NonEmptyCollection.Invoking(value => value.DoesNotContainValue(GetItemValue()))
                              .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DoesNotContainValue_should_pass()
        {
            EmptyCollection.Invoking(value => value.DoesNotContainValue(GetItemValue()))
                           .ShouldNotThrow();
        }
    }
}