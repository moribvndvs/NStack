#region header
// <copyright file="NHLogger.cs" company="mikegrabski.com">
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

using NHibernate;

namespace NStack.Logging
{
    /// <summary>
    /// An implementation of <see cref="IInternalLogger"/> that uses Common.Logging.
    /// </summary>
    public class NHLogger : IInternalLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public NHLogger(ILog log)
        {
            _log = log;
        }

        private readonly ILog _log;

        #region Implementation of IInternalLogger

        public void Error(object message)
        {
            _log.Error(message == null ? null : message.ToString());
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(exception, message == null ? null : message.ToString());
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.Error(format, args);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message == null ? null : message.ToString());
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(exception, message == null ? null : message.ToString());
        }

        public void Debug(object message)
        {
            _log.Debug(message == null ? null : message.ToString());
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(exception, message == null ? null : message.ToString());
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.Debug(format, args);
        }

        public void Info(object message)
        {
            _log.Info(message == null ? null : message.ToString());
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(exception, message == null ? null : message.ToString());
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.Info(format, args);
        }

        public void Warn(object message)
        {
            _log.Warn(message == null ? null : message.ToString());
        }

        public void Warn(object message, Exception exception)
        {
            _log.Warn(exception, message == null ? null : message.ToString());
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.Warn(format, args);
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsEnabled(LogLevel.Error); }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsEnabled(LogLevel.Fatal); }
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsEnabled(LogLevel.Debug); }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsEnabled(LogLevel.Info); }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsEnabled(LogLevel.Warn); }
        }

        #endregion
    }
}