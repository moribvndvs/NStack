#region header
// <copyright file="FlakeGeneratorDef.cs" company="mikegrabski.com">
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

using NHibernate.Mapping.ByCode;

namespace NStack.Data
{
    public class FlakeGeneratorDef : IGeneratorDef
    {
        #region Implementation of IGeneratorDef

        public static readonly FlakeGeneratorDef Instance = new FlakeGeneratorDef();

        public string Class { get { return typeof(FlakeGenerator).AssemblyQualifiedName; } }

        public object Params { get { return null; } }

        public Type DefaultReturnType { get { return typeof(decimal); } }

        public bool SupportedAsCollectionElementId { get { return true; } }

        #endregion
    }
}