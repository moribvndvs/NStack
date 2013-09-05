#region header

// <copyright file="LogLevel.cs" company="mikegrabski.com">
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
    ///     Indicates the importance of a message during logging.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Everything.
        /// </summary>
        Verbose,

        /// <summary>
        /// Information useful during debugging.
        /// </summary>
        Debug,

        /// <summary>
        /// Generally useful information.
        /// </summary>
        Info,

        /// <summary>
        /// Maybe something isn't working as expected, but let's not freak out yet.
        /// </summary>
        Warn,

        /// <summary>
        /// Something definitely isn't working.
        /// </summary>
        Error,

        /// <summary>
        /// OK, it's time to freak out.
        /// </summary>
        Fatal
    }
}