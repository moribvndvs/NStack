#region header
// <copyright file="TypeExtensions.ImplementsInterfaceDirectly.cs" company="mikegrabski.com">
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
using System.Linq;

namespace NStack.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class TypeExtensions
    {
        /// <summary>
        /// Returns whether or not the specified interface is directly implemented by the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="theInterface">The type of the interface.</param>
        /// <returns>True, if <paramref name="type"/> directly implements <paramref name="theInterface"/>; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theInterface"/> or <paramref name="type"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="theInterface"/> is not the type of an interface.</exception>
        public static bool ImplementsInterfaceDirectly(this Type type, Type theInterface)
        {
            Requires.That(type, "type")
                .IsNotNull();

            Requires.That(theInterface, "theInterface")
                .IsNotNull()
                .IsMet(theInterface.IsInterface, "Must be the Type of an interface.");

            if (type.BaseType != null 
                && ImplementsInterfaceDirectly(type.BaseType, theInterface))
                return false;

            return !type.GetInterfaces().Any(i => ImplementsInterfaceDirectly(i, theInterface))
                   && type.GetInterfaces().Any(i => i == theInterface);
        }
    }
}