#region header
// <copyright file="AutoMapperTestEntities.cs" company="mikegrabski.com">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using NStack.Models;

namespace NStack.Data
{
    public abstract class AutoMapperTestEntityBase<T> : Entity<T>
    {

    }

    public class ParentWithGuid : AutoMapperTestEntityBase<Guid>
    {

    }

    public abstract class AutoMapperTestEntityBase : Entity
    {

    }

    public class Address
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }

    public class Parent : AutoMapperTestEntityBase
    {
        public string FirstName { get; set; }

        public Address Address { get; set; }

        public int NotNullableValue { get; set; }

        public int? NullableValue { get; set; }

        public Parent NullableReference { get; set; }

        [Required]
        public Parent NotNullableReference { get; set; }

        public IList<Child> BagChildren { get; set; }
    }

    public class Child : AutoMapperTestEntityBase
    {
        public Parent BagParent { get; set; }
    }

    public class JoinedSubclassedParent : Parent
    {

    }
}