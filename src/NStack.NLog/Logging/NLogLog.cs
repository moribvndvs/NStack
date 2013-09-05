#region header
// <copyright file="NLogLog.cs" company="mikegrabski.com">
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
using System.Globalization;

using NLog;

namespace NStack.Logging
{
    /// <summary>
    /// An implementation of <see cref="ILog"/> that wraps <see cref="Logger"/>.
    /// </summary>
    public class NLogLog : ILog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public NLogLog(Logger logger)
        {
            Logger = logger;
        }

        protected Logger Logger { get; private set; }

        #region Implementation of ILog

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="level">The LogLevel.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">Values that will be incorporated into the message.</param>
        public void Write(LogLevel level, string message, params object[] values)
        {
            Logger.Log(ToNLogLevel(level), message, values);
        }

        /// <summary>
        /// Writes an exception and message to the log.
        /// </summary>
        /// <param name="level">The LogLevel.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">Values that will be incorporated into the message.</param>
        public void Write(LogLevel level, Exception exception, string message, params object[] values)
        {
            Logger.LogException(ToNLogLevel(level), string.Format(CultureInfo.CurrentCulture, message, values),
                                exception);
        }

        /// <summary>
        /// Returns whether or not the specified <see cref="LogLevel"/> is enabled for the log.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <returns>True if <paramref name="level"/> is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogLevel level)
        {
            return Logger.IsEnabled(ToNLogLevel(level));
        }

        #endregion

        private static NLog.LogLevel ToNLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Info:
                    return NLog.LogLevel.Info;
                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }
    }
}