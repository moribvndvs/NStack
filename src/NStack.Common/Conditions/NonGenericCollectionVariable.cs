#region header
// <copyright file="NonGenericCollectionVariable.cs" company="mikegrabski.com">
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStack.Conditions
{
    public class NonGenericCollectionVariable : CollectionVariable<IEnumerable, NonGenericCollectionVariable>
    {
        private IEnumerable<object> _linqValue;

        protected IEnumerable<object> LinqValue
        {
            get { return _linqValue ?? (_linqValue = Value.Cast<object>()); }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public NonGenericCollectionVariable(IEnumerable value, string name, bool postCondition) : base(value, name, postCondition)
        {
        }

        #region Overrides of CollectionVariable<IEnumerable,NonGenericCollectionVariable>

        protected override bool HasAny()
        {
            return LinqValue.Any();
        }

        protected override int GetCount()
        {
            return LinqValue.Count();
        }

        #endregion
    }
}