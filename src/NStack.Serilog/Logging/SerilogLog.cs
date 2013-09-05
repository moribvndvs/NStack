#region header

// <copyright file="SerilogLog.cs" company="mikegrabski.com">
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

using Serilog;
using Serilog.Events;

namespace NStack.Logging
{
    /// <summary>
    /// An implementation of <see cref="ILog"/> that wraps <see cref="ILogger"/>, and doubles as an <see cref="ILogProvider"/>.
    /// </summary>
    public class SerilogLog : ILog, ILogProvider
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public SerilogLog(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// The Serilog <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; private set; }

        #region Implementation of ILog

        /// <summary>
        ///     Writes a message to the log.
        /// </summary>
        /// <param name="level">The LogLevel.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">Values that will be incorporated into the message.</param>
        public void Write(LogLevel level, string message, params object[] values)
        {
            Logger.Write(ToLogEventLevel(level), message, values);
        }

        /// <summary>
        ///     Writes an exception and message to the log.
        /// </summary>
        /// <param name="level">The LogLevel.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="values">Values that will be incorporated into the message.</param>
        public void Write(LogLevel level, Exception exception, string message, params object[] values)
        {
            Logger.Write(ToLogEventLevel(level), exception, message, values);
        }

        /// <summary>
        ///     Returns whether or not the specified <see cref="LogLevel" /> is enabled for the log.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <returns>
        ///     True if <paramref name="level" /> is enabled; otherwise, false.
        /// </returns>
        public bool IsEnabled(LogLevel level)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static LogEventLevel ToLogEventLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    return LogEventLevel.Verbose;
                case LogLevel.Debug:
                    return LogEventLevel.Debug;
                case LogLevel.Info:
                    return LogEventLevel.Information;
                case LogLevel.Warn:
                    return LogEventLevel.Warning;
                case LogLevel.Error:
                    return LogEventLevel.Error;
                case LogLevel.Fatal:
                    return LogEventLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        #region Implementation of ILogProvider

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified type.
        /// </summary>
        /// <typeparam name="T">The type the <see cref="ILog"/> will log messages for.</typeparam>
        /// <returns>An <see cref="ILog"/>.</returns>
        public ILog Get<T>()
        {
            return new SerilogLog(Logger.ForContext<T>());
        }

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified type.
        /// </summary>
        /// <param name="type">The type the <see cref="ILog"/> will log message for.</param>
        /// <returns>An <see cref="ILog"/>.</returns>
        public ILog Get(Type type)
        {
            return new SerilogLog(Logger.ForContext(type));
        }

        /// <summary>
        /// Returns an <see cref="ILog"/> for the specified key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ILog Get(string key)
        {
            return new SerilogLog(Logger.ForContext("SourceContext", key, false));
        }

        #endregion
    }
}