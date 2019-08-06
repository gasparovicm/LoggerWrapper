using idev.LoggerWrapper;
using NUnit.Framework;

namespace idevUnitTests.Log
{
    [TestFixture]
    public class LogManagerUnitTests
    {
        [SetUp]
        public void SetUpAllTests()
        {
        }

        [Test]
        public void LogManager_SetLoggerType_Test()
        {
            //arrange

            LogManager.SetLoggerType(typeof(NoOpLogger));
            //act
            var logger = LogManager.GetLogger("test");
            //assert
            Assert.AreEqual(typeof(NoOpLogger), logger.GetType());
        }

        [Test]
        public void LogManager_GetLoggerWithLevels_TypeTest()
        {
            //arrange

            LogManager.SetLoggerType(typeof(NoOpLogger));
            //act
            var logger = LogManager.GetLoggerWithLevels("test");
            //assert
            Assert.IsInstanceOf<ILoggerWithLevels>(logger);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

    }
}
