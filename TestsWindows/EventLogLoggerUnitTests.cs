using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using idev.LoggerWrapper;
using idev.LoggerWrapper.Windows;
using NUnit.Framework;

namespace idevUnitTests.Log
{
    [TestFixture]
    public class EventLogLoggerUnitTests 
    {
        #region Levels tests

        [Test]
        public void EventLogLogger_InfoLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x,y,z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x,y,z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsInfoEnabled = true;
            logger.Info(testMessage);
            logger.Info(testException);
            logger.Info(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Information));
        }


        [Test]
        public void EventLogLogger_TraceLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x, y, z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x, y, z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsTraceEnabled = true;
            logger.Trace(testMessage);
            logger.Trace(testException);
            logger.Trace(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Information));
        }

        [Test]
        public void EventLogLogger_DebugLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x, y, z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x, y, z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsDebugEnabled = true;
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Information));
        }

        [Test]
        public void EventLogLogger_WarnLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x, y, z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x, y, z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsWarnEnabled = true;
            logger.Warn(testMessage);
            logger.Warn(testException);
            logger.Warn(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Warning));
        }

        [Test]
        public void EventLogLogger_ErrorLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x, y, z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x, y, z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsErrorEnabled = true;
            logger.Error(testMessage);
            logger.Error(testException);
            logger.Error(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Error));
        }

        [Test]
        public void EventLogLogger_FatalLevel_EventLogEntryTypeUsedTest()
        {
            //Arrange
            var messages = new List<Tuple<string, string, EventLogEntryType>>();
            var logger = new EventLogLogger("Test")
            {
                WriteEntryToEventLog = (x, y, z) => messages.Add(new Tuple<string, string, EventLogEntryType>(x, y, z))
            };
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsFatalEnabled = true;
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Select(x => x).Count(x => x.Item3 == EventLogEntryType.Error));
        }
        #endregion Levels tests

        #region MessageExposedTests
        [Test]
        public void EventLogLogger_InfoLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsInfoEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Info(testMessage);
            logger.Info(testException);
            logger.Info(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Info, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Info, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Info, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }

        [Test]
        public void EventLogLogger_TraceLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsTraceEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Trace(testMessage);
            logger.Trace(testException);
            logger.Trace(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Trace, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Trace, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Trace, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }

        [Test]
        public void EventLogLogger_DebugLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsDebugEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Debug, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Debug, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Debug, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }

        [Test]
        public void EventLogLogger_WarnLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsWarnEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Warn(testMessage);
            logger.Warn(testException);
            logger.Warn(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Warn, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Warn, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Warn, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }

        [Test]
        public void EventLogLogger_ErrorLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsErrorEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Error(testMessage);
            logger.Error(testException);
            logger.Error(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Error, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Error, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Error, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }

        [Test]
        public void EventLogLogger_FatalLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new EventLogLogger("Test");
            logger.IsFatalEnabled = true;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);
            //Assert
            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Fatal, testMessage, null), messages[0], "Log entry with message was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Fatal, string.Empty, testException), messages[1], "Log entry with exception was not exposed correctly.");
            Assert.AreEqual(new Tuple<LogLevel, string, Exception>(LogLevel.Fatal, testMessage, testException), messages[2], "Log entry with message and exception was not exposed correctly.");
        }
        #endregion MessageExposedTests
    }
}
