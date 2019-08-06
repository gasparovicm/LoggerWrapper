using System;
using System.Collections.Generic;
using System.Globalization;
using idev.LoggerWrapper;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Log4Net.Universal;
using Moq;
using NUnit.Framework;
using LogLevel = idev.LoggerWrapper.LogLevel;

namespace idevUnitTests.Log
{

    [TestFixture]
    public class LoggerWrapperUnitTests
    {
        private static Mock<ILog> loggerMock;
        private static ILog log4Net;
        [OneTimeSetUp]
        public void FirstSetUp()
        {
            loggerMock = CreateLoggerMockWithMethods();

            var log4NetAdapter = new Log4NetFactoryAdapter(new NameValueCollection());
            log4net.Config.XmlConfigurator.Configure();
            log4Net = log4NetAdapter.GetLogger("test");
        }

        private static Mock<ILog> CreateLoggerMockWithMethods()
        {
            var mock = new Mock<ILog>();
            SetUpLoggerMockMethods(mock);
            return mock;
        }

        private static void SetUpLoggerMockMethods(Mock<ILog> loggerMockParameter)
        {
            loggerMockParameter.Setup(x => x.Info(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Info(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Info(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.InfoFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));

            loggerMockParameter.Setup(x => x.Trace(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Trace(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Trace(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.TraceFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));

            loggerMockParameter.Setup(x => x.Debug(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Debug(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Debug(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.DebugFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));

            loggerMockParameter.Setup(x => x.Warn(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Warn(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.WarnFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));

            loggerMockParameter.Setup(x => x.Error(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Error(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.ErrorFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));

            loggerMockParameter.Setup(x => x.Fatal(It.IsAny<string>()));
            loggerMockParameter.Setup(x => x.Fatal(It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.Fatal(It.IsAny<string>(), It.IsAny<Exception>()));
            loggerMockParameter.Setup(x => x.FatalFormat(It.IsAny<CultureInfo>(), It.IsAny<string>(), It.IsAny<object[]>()));
        }

        [SetUp]
        public void SetUpAllTests()
        {
        }

        #region Logger_CreateFromILogLoggerMock
        [Test]
        public void Logger_CreateFromILogLoggerMock_InfoMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Info(testMessage);
            logger.Info(testException);
            logger.Info(testMessage,testException);

            //Assert
            loggerMock.Verify(x => x.Info(It.IsAny<string>()),Times.AtLeastOnce);
            loggerMock.Verify(x => x.Info(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Info(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_TraceMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Trace(testMessage);
            logger.Trace(testException);
            logger.Trace(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Trace(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Trace(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Trace(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_DebugMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            //Act

            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Debug(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_WarnMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Warn(testMessage);
            logger.Warn(testException);
            logger.Warn(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Warn(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_ErrorMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Error(testMessage);
            logger.Error(testException);
            logger.Error(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Error(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_FatalMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Fatal(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Fatal(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Fatal(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        #endregion Logger_CreateFromILogLoggerMock

        #region Logger_CreateFromILogLikeObject
        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_InfoMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Info(testMessage);
            logger.Info(testException);
            logger.Info(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }

        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_TraceMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Trace(testMessage);
            logger.Trace(testException);
            logger.Trace(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }

        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_DebugMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }

        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_WarnMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Warn(testMessage);
            logger.Warn(testException);
            logger.Warn(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }

        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_ErrorMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Error(testMessage);
            logger.Error(testException);
            logger.Error(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }

        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_FatalMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(log4Net);
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }
        #endregion Logger_CreateFromILogLoggerLog4Net

        #region CreateFrom ILog 
        [Test]
        public void Logger_CreateFromILog_DebugMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            var logger = new LoggerWrapperCommonLog(log4Net);
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            //Act
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);

            //Assert
            Assert.AreEqual(3, messages.Count);

        }
        #endregion

        #region CreateFrom LogMethods
        [Test]
        public void Logger_CreateFromLogMethods_DebugMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());
            var logMethodsMock = new Mock<ILogMethods>();
            //Act
            var logger = new LoggerWrapper(logMethodsMock.Object,"test");
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            logger.LogMessageHandler += (x, y, z) => messages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));
            logger.Debug(testMessage);
            logger.Debug(testException);
            logger.Debug(testMessage, testException);

            //Assert
            Assert.AreEqual(3, messages.Count);

        }
        #endregion

        #region Format methods
        [Test]
        public void Logger_CreateFromILogLoggerMock_InfoFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Info(It.IsAny<string>())).Callback((object x)=>retrievedMessage=(string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.InfoFormat(CultureInfo.CurrentCulture, "test{0}",1);

            //Assert
            localLoggerMock.Verify(x => x.Info(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }
        [Test]
        public void Logger_CreateFromILogLoggerMock_DebugFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Debug(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.DebugFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_TraceFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Trace(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.TraceFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Trace(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }


        [Test]
        public void Logger_CreateFromILogLoggerMock_WarnFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Warn(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.WarnFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_ErrorFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Error(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.ErrorFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Error(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void Logger_CreateFromILogLoggerMock_FatalFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Fatal(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.FatalFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Fatal(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }
        #endregion 

        #region Simple constructors
        // internal methods
        //[Test]
        //public void Logger_CreateFromWithNameTest()
        //{
        //    //Arrange

        //    //Act
        //    var logger = new Logger("test");
        //    //Assert
        //}

        //[Test]
        //public void Logger_CreateFromWithTypeTest()
        //{
        //    //Arrange

        //    //Act
        //    var logger = new Logger(this.GetType());
        //    //Assert
        //}
        #endregion Simple constructors

        #region Message exposed tests
        [Test]
        public void LoggerWrapper_InfoLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        public void LoggerWrapper_TraceLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        public void LoggerWrapper_DebugLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        public void LoggerWrapper_WarnLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        public void LoggerWrapper_ErrorLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        public void LoggerWrapper_FatalLog_MessageExposedTest()
        {
            //Arrange
            var messages = new List<Tuple<LogLevel, string, Exception>>();
            var logger = LoggerWrapper.CreateFromILogLikeObject(CreateLoggerMockWithMethods().Object);
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
        #endregion Message exposed tests

        [TearDown]
        public void TearDown()
        {

        }

    }
}
