using System;
using System.Globalization;
using idev.LoggerWrapper;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Log4Net.Universal;
using Moq;
using NUnit.Framework;

namespace idevUnitTests.Log
{

    [TestFixture]
    public class LoggerUnitTests
    {
        private static Mock<ILog> loggerMock;
        private static ILog log4Net;
        [OneTimeSetUp]
        public void FirstSetUp()
        {
            loggerMock = CreateLoggerMockWithMethods();

            var log4NetAdapter = new Log4NetFactoryAdapter(new NameValueCollection());
            log4Net = log4NetAdapter.GetLogger("test");
        }

        private static Mock<ILog> CreateLoggerMockWithMethods()
        {
            var mock = new Mock<ILog>();
            SetUpLoggerMockMethods(mock);
            mock.SetupAllProperties();
            mock.SetupGet(x => x.IsInfoEnabled).Returns(true);
            mock.SetupGet(x => x.IsTraceEnabled).Returns(true);
            mock.SetupGet(x => x.IsDebugEnabled).Returns(true);
            mock.SetupGet(x => x.IsWarnEnabled).Returns(true);
            mock.SetupGet(x => x.IsErrorEnabled).Returns(true);
            mock.SetupGet(x => x.IsFatalEnabled).Returns(true);

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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(loggerMock.Object);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(loggerMock.Object);//CreateFromLog4Net
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(loggerMock.Object);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(loggerMock.Object);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(loggerMock.Object);
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);

            //Assert
            loggerMock.Verify(x => x.Fatal(It.IsAny<string>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Fatal(It.IsAny<Exception>()), Times.AtLeastOnce);
            loggerMock.Verify(x => x.Fatal(It.IsAny<string>(), It.IsAny<Exception>()), Times.AtLeastOnce);
        }

        #endregion Logger_CreateFromILogLoggerMock

        #region Logger_CreateFromILogLoggerLog4Net
        [Test]
        public void Logger_CreateFromILogLoggerLog4Net_InfoMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(log4Net);
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }
        #endregion Logger_CreateFromILogLoggerLog4Net

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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(localLoggerMock.Object);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);//CreateFromLog4Net
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);//CreateFromLog4Net
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(localLoggerMock.Object);
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(localLoggerMock.Object);

            //Act
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
            var logger = LoggerWrapperCommonLog.CreateFromLog4Net(localLoggerMock.Object);
            logger.FatalFormat(CultureInfo.CurrentCulture, "test{0}", 1);

            //Assert
            localLoggerMock.Verify(x => x.Fatal(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }
        #endregion Format methods

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

        [TearDown]
        public void TearDown()
        {

        }

    }
}
