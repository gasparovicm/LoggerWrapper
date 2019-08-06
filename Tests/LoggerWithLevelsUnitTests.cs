using System;
using System.Globalization;
using idev.LoggerWrapper;
using Moq;
using NUnit.Framework;

namespace idevUnitTests.Log
{
    [TestFixture]
    public class LoggerWithLevelsUnitTests
    {

        [SetUp]
        public void SetUpAllTests()
        {
        }

        #region LogWithMessage
        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_LogWithLevelTest()
        {
            //arrange
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object);
            //act
            loggerWithLevels.Log(LogLevel.Warn, "test");
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_ExposedInfoLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Info;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Info(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Error;
            string retrievedMessage = string.Empty;

            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageExposedDebugLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Debug(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object,LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;

            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
            {
                retrievedLogLevel = x;
                retrievedMessage = y;

            };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage,retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_ExposedTraceLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Trace;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Trace(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_ExposedWarnLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_ExposedErrorLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Error;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Error(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;

            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_ExposedFatalLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Fatal;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Fatal(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                };
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_LevelLimitationTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);

            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.Log(requiredLogLevel, testMessage);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_LevelLimitationWithPropertySetTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);
            
            var testMessage = Guid.NewGuid().ToString();

            //act
            loggerWithLevels.LogLevel= LogLevel.Trace;
            loggerWithLevels.Log(requiredLogLevel, testMessage);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion LogWithMessage

        #region LogWithException

        [Test]
        public void LoggerWithLevels_LogEntryWithException_LogWithLevelTest()
        {
            //arrange
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object);
            //act
            loggerWithLevels.Log(LogLevel.Warn, new Exception("test"));
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedInfoLog_Test()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Info;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Info( It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Error;

            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;

                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel,testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedDebugLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Debug(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedTraceLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Trace;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Trace(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedWarnLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn( It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel,testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedErrorLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Error;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Error(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithException_ExposedFatalLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Fatal;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Fatal(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedException = z;
                };
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogWithException_LevelLimitationTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);

            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredLogLevel, testException);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        public void LoggerWithLevels_LogWithException_LevelLimitationWithPropertySetTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);

            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.LogLevel = LogLevel.Trace;
            loggerWithLevels.Log(requiredLogLevel, testException);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        #endregion LogWithMessageAndException

        #region LogWithMessageAndException
        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_LogWithLevelTest()
        {
            //arrange
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>(),It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object);
            //act
            loggerWithLevels.Log(LogLevel.Warn,"message", new Exception("test"));
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>(),It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedInfoLog_Test()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Info;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Info(It.IsAny<string>(),It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Error;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedDebugLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Debug(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedTraceLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Trace;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Trace(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedWarnLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedErrorLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Error;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_ExposedFatalLogTest()
        {
            //arrange
            var requiredDebugLevel = LogLevel.Fatal;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Fatal(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Trace);

            LogLevel retrievedLogLevel = LogLevel.Info;
            string retrievedMessage = string.Empty;
            Exception retrievedException = null;
            loggerWithLevels.LogMessageHandler +=
                (x, y, z) =>
                {
                    retrievedLogLevel = x;
                    retrievedMessage = y;
                    retrievedException = z;
                };
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredDebugLevel, testMessage, testException);
            //assert
            Assert.AreEqual(requiredDebugLevel, retrievedLogLevel);
            Assert.AreEqual(testMessage, retrievedMessage);
            Assert.AreEqual(testException, retrievedException);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_LevelLimitationTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Debug;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);

            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.Log(requiredLogLevel, testMessage, testException);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        public void LoggerWithLevels_LogEntryWithMessageAndException_LevelLimitationWithPropertySetTest()
        {
            //arrange
            var requiredLogLevel = LogLevel.Warn;
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object, LogLevel.Error);

            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //act
            loggerWithLevels.LogLevel = LogLevel.Trace;
            loggerWithLevels.Log(requiredLogLevel, testMessage, testException);
            //assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        #endregion LogWithMessageAndException

        #region LogFormatWithMessage
        [Test]
        public void LoggerWithLevels_LogEntryWithMessage_LogFormatWithLevelTest()
        {//(CultureInfo culture, string message, params object[] objects)
            //arrange
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Warn(It.IsAny<string>()));
            var loggerWithLevels = new LoggerWithLevels(loggerMock.Object);
            //act
            loggerWithLevels.LogFormat(LogLevel.Warn,CultureInfo.CurrentCulture, "test",new object[0]);
            //assert
            loggerMock.Verify(x => x.Warn( It.IsAny<string>()),  Times.AtLeastOnce);
        }
        #endregion LogFormatWithMessage

        [TearDown]
        public void TearDown()
        {

        }
    }
}
