using System;
using System.Collections.Generic;
using idev.LoggerWrapper;
using NUnit.Framework;

namespace idevUnitTests.Log
{
    public class NoOpLoggerTests
    {
        #region Levels tests

        [Test]
        public void NoOpLogger_DisableInfoLevel_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.IsInfoEnabled = false;
            logger.Info(testMessage);
            logger.Info(testException);
            logger.Info(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);            
        }


        [Test]
        public void NoOpLogger_DisableTraceLevel_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.IsTraceEnabled = false;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Trace(testMessage);
            logger.Trace(testException);
            logger.Trace(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);
         }

        [Test]
        public void NoOpLogger_DisableDebugLog_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.IsDebugEnabled = false;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);
        }

        [Test]
        public void NoOpLogger_DisableWarnLog_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.IsWarnEnabled = false;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Warn(testMessage);
            logger.Warn(testException);
            logger.Warn(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);
        }

        [Test]
        public void NoOpLogger_DisableErrorLog_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.IsErrorEnabled = false;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Error(testMessage);
            logger.Error(testException);
            logger.Error(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);
        }

        [Test]
        public void NoOpLogger_DisableFatalLog_MessageNotExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
            logger.IsFatalEnabled = false;
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            var testMessage = "Test message";
            var testException = new Exception("Test");
            //Act
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);
            //Assert
            Assert.AreEqual(0, messages.Count);
        }
        #endregion Levels tests

        #region MessageExposedTests
        [Test]
        public void NoOpLogger_InfoLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
        public void NoOpLogger_TraceLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
        public void NoOpLogger_DebugLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
        public void NoOpLogger_WarnLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
        public void NoOpLogger_ErrorLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
        public void NoOpLogger_FatalLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = new NoOpLogger("Test");
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
