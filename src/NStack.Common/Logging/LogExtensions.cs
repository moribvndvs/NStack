#region header
// <copyright file="LogExtensions.cs" company="mikegrabski.com">
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

namespace NStack.Logging
{
    /// <summary>
    /// Defines extensions for <see cref="ILog"/>
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Verbose"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Verbose(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Verbose, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Verbose"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Verbose(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Verbose, exception, message, values);
        }
        
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Debug(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Debug, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Debug"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Debug(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Debug, exception, message, values);
        }
        
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Info(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Info, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Info"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Info(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Info, exception, message, values);
        }
        
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Warn(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Warn, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Warn"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Warn(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Warn, exception, message, values);
        }
        
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Error(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Error, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Error(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Error, exception, message, values);
        }
        
        /// <summary>
        /// Writes a message to the log as <see cref="LogLevel.Fatal"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Fatal(this ILog log, string message, params object[] values)
        {
            log.Write(LogLevel.Fatal, message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log as <see cref="LogLevel.Error"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message.</param>
        /// <param name="values">The values used in the message.</param>
        public static void Fatal(this ILog log, Exception exception, string message, params object[] values)
        {
            log.Write(LogLevel.Fatal, exception, message, values);
        }


    }
}