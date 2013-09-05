#region header
// <copyright file="LogProvider.cs" company="mikegrabski.com">
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
namespace NStack.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LogProvider
    {
        private static ILogProvider _provider = new NullLogProvider();

        /// <summary>
        /// Gets or sets the log provider.
        /// </summary>
        public static ILogProvider Provider
        {
            get { return _provider; }
            set
            {
                Requires.That(value).IsNotNull("An ILogProvider must be specified.");
                _provider = value;
            }
        }
    }
}