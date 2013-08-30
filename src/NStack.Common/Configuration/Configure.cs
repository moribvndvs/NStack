#region header
// <copyright file="Configure.cs" company="mikegrabski.com">
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
namespace NStack.Configuration
{
    /// <summary>
    /// A static facade for fluently configuring an application.
    /// </summary>
    public static class Configure
    {
        /// <summary>
        /// Begins configuration using the specified container.
        /// </summary>
        /// <param name="containerAdapter">The container adapter.</param>
        /// <returns>The current config.</returns>
        public static IConfiguration Using(IContainerAdapter containerAdapter)
        {
            Requires.That(containerAdapter).IsNotNull();

            return new DefaultConfiguration(containerAdapter);
        }
    }
}