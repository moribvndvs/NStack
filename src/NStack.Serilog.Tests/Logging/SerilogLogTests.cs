#region header
// <copyright file="SerilogLogTests.cs" company="mikegrabski.com">
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

using NUnit.Framework;

using Moq;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace NStack.Logging
{
    [TestFixture]
    public class SerilogLogTests
    {
        #region Set-up and Tear-down

        [TestFixtureSetUp]
        public void SetUpFixture()
        {

        }

        [SetUp]
        public void SetUpTest()
        {

        }

        [TearDown]
        public void TearDownTest()
        {

        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {

        }

        #endregion

        private static SerilogLog CreateLogger(ILogEventSink sink)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(sink)
                .CreateLogger();

            return new SerilogLog(logger);


        }

        private static Mock<ILogEventSink> CreateSink(string sourceContext)
        {
            var sink = new Mock<ILogEventSink>();
            sink.Setup(
                c =>
                c.Emit(It.Is<LogEvent>(@event => @event.Properties["SourceContext"].ToString().Trim('"') == sourceContext)));
            return sink;
        }

        [Test]
        public void Get_should_include_source_context_for_key()
        {
            // Arrange
            var sink = CreateSink(GetType().Name);
            var log = CreateLogger(sink.Object);

            // Act
            log.Get(GetType().Name).Debug("{index} - this is a test {now}", DateTime.Now, 1);

            // Assert
            sink.VerifyAll();

        }



        [Test]
        public void Get_should_include_source_context_for_type()
        {
            // Arrange
            var sink = CreateSink(GetType().FullName);
            var log = CreateLogger(sink.Object);

            // Act
            log.Get(GetType()).Debug("{index} - this is a test {now}", DateTime.Now, 1);

            // Assert
            sink.VerifyAll();

        }
        
        [Test]
        public void Get_should_include_source_context_for_type_parameter()
        {
            // Arrange
            var sink = CreateSink(GetType().FullName);
            var log = CreateLogger(sink.Object);

            // Act
            log.Get<SerilogLogTests>().Debug("{index} - this is a test {now}", DateTime.Now, 1);

            // Assert
            sink.VerifyAll();

        }
    }
}