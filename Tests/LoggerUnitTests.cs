using System;
using System.Globalization;
using idev.LoggerWrapper;
using Moq;
using NUnit.Framework;

namespace idevUnitTests.Log
{

    [TestFixture]
    public class LoggerUnitTests
    {
        private static Mock<ILogger> loggerMock;
        private static ILogger testLogger;

        [OneTimeSetUp]
        public void FirstSetUp()
        {
            loggerMock = new Mock<ILogger>();

            testLogger = new TestLogger("test");
        }

        private static Mock<ILogger> CreateLoggerMockWithMethods()
        {
            var mock = new Mock<ILogger>();
            SetUpLoggerMockMethods(mock);
            return mock;
        }

        private static void SetUpLoggerMockMethods(Mock<ILogger> loggerMockParameter)
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

        #region Logger_CreateFromILoggerMock
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
        public void Logger_CreateFromILoggerLog4Net_InfoMethodsTest()
        {
            //Arrange
            var testMessage = Guid.NewGuid().ToString();
            var testException = new Exception(Guid.NewGuid().ToString());

            //Act
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
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
            var logger = LoggerWrapper.CreateFromILogLikeObject(testLogger);
            logger.Fatal(testMessage);
            logger.Fatal(testException);
            logger.Fatal(testMessage, testException);

            //Assert
            Assert.Pass(); // just exception can appear in code above
        }
        #endregion Logger_CreateFromILogLoggerLog4Net

        #region Format methods
        [Test]
        public void Logger_CreateFromILoggerMock_InfoFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Info(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act            
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);
            logger.InfoFormat(CultureInfo.CurrentCulture, "test{0}", 1);
            //Assert
            localLoggerMock.Verify(x => x.Info(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void Logger_CreateFromILoggerMock_DebugFormatMethodTest()
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
        public void Logger_CreateFromILoggerMock_TraceFormatMethodTest()
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
        public void Logger_CreateFromILoggerMock_WarnFormatMethodTest()
        {
            //Arrange
            var testMessage = "test1";
            var retrievedMessage = string.Empty;
            var localLoggerMock = CreateLoggerMockWithMethods();
            localLoggerMock.Setup(x => x.Warn(It.IsAny<string>())).Callback((object x) => retrievedMessage = (string)x);
            //Act            
            var logger = LoggerWrapper.CreateFromILogLikeObject(localLoggerMock.Object);//CreateFromLog4Net
            logger.WarnFormat(CultureInfo.CurrentCulture, "test{0}", 1);
            //Assert
            localLoggerMock.Verify(x => x.Warn(It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(testMessage, retrievedMessage);
        }

        [Test]
        public void Logger_CreateFromILoggerMock_ErrorFormatMethodTest()
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
        public void Logger_CreateFromILoggerMock_FatalFormatMethodTest()
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
