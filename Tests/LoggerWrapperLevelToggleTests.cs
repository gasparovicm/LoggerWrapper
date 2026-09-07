using System;
using System.Collections.Generic;
using idev.LoggerWrapper;
using NUnit.Framework;
using LogLevel = idev.LoggerWrapper.LogLevel;

namespace idevUnitTests.Log
{
    /// <summary>
    /// Regression tests for the level toggles on LoggerWrapper. Before the fix the class overrode only the
    /// getters of the IsXxxEnabled properties, so an assignment wrote a LoggerBase field that no getter read
    /// and was silently ignored.
    /// </summary>
    [TestFixture]
    public class LoggerWrapperLevelToggleTests
    {
        private List<string> writtenMessages;
        private List<Tuple<LogLevel, string, Exception>> exposedMessages;

        private static readonly LogLevel[] Levels =
        {
            LogLevel.Fatal, LogLevel.Error, LogLevel.Warn, LogLevel.Trace, LogLevel.Debug, LogLevel.Info
        };

        [SetUp]
        public void SetUp()
        {
            writtenMessages = new List<string>();
            exposedMessages = new List<Tuple<LogLevel, string, Exception>>();
        }

        private LogMethods CreateRecordingLogMethods(bool isEnabled)
        {
            return new LogMethods(
                x => writtenMessages.Add(x),
                x => writtenMessages.Add(x.Message),
                (x, y) => writtenMessages.Add(x + y.Message),
                isEnabled);
        }

        /// <summary>
        /// Builds a wrapper whose six levels all use recording log methods in the given state.
        /// </summary>
        private LoggerWrapper CreateLogger(bool isEnabled)
        {
            return new LoggerWrapper(
                CreateRecordingLogMethods(isEnabled),
                CreateRecordingLogMethods(isEnabled),
                CreateRecordingLogMethods(isEnabled),
                CreateRecordingLogMethods(isEnabled),
                CreateRecordingLogMethods(isEnabled),
                CreateRecordingLogMethods(isEnabled));
        }

        private static void WriteAllOverloads(LoggerWrapper logger, LogLevel level)
        {
            var exception = new Exception("exception");
            switch (level)
            {
                case LogLevel.Fatal:
                    logger.Fatal("message");
                    logger.Fatal(exception);
                    logger.Fatal("message", exception);
                    break;
                case LogLevel.Error:
                    logger.Error("message");
                    logger.Error(exception);
                    logger.Error("message", exception);
                    break;
                case LogLevel.Warn:
                    logger.Warn("message");
                    logger.Warn(exception);
                    logger.Warn("message", exception);
                    break;
                case LogLevel.Trace:
                    logger.Trace("message");
                    logger.Trace(exception);
                    logger.Trace("message", exception);
                    break;
                case LogLevel.Debug:
                    logger.Debug("message");
                    logger.Debug(exception);
                    logger.Debug("message", exception);
                    break;
                case LogLevel.Info:
                    logger.Info("message");
                    logger.Info(exception);
                    logger.Info("message", exception);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        private static void SetLevelEnabled(LoggerWrapper logger, LogLevel level, bool isEnabled)
        {
            switch (level)
            {
                case LogLevel.Fatal:
                    logger.IsFatalEnabled = isEnabled;
                    break;
                case LogLevel.Error:
                    logger.IsErrorEnabled = isEnabled;
                    break;
                case LogLevel.Warn:
                    logger.IsWarnEnabled = isEnabled;
                    break;
                case LogLevel.Trace:
                    logger.IsTraceEnabled = isEnabled;
                    break;
                case LogLevel.Debug:
                    logger.IsDebugEnabled = isEnabled;
                    break;
                case LogLevel.Info:
                    logger.IsInfoEnabled = isEnabled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        private static bool GetLevelEnabled(LoggerWrapper logger, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Fatal:
                    return logger.IsFatalEnabled;
                case LogLevel.Error:
                    return logger.IsErrorEnabled;
                case LogLevel.Warn:
                    return logger.IsWarnEnabled;
                case LogLevel.Trace:
                    return logger.IsTraceEnabled;
                case LogLevel.Debug:
                    return logger.IsDebugEnabled;
                case LogLevel.Info:
                    return logger.IsInfoEnabled;
                default:
                    throw new ArgumentOutOfRangeException("level");
            }
        }

        [Test]
        public void Logger_LevelSetToFalse_NothingIsWrittenOrExposed([ValueSource(nameof(Levels))] LogLevel level)
        {
            //Arrange
            var logger = CreateLogger(true);
            logger.LogMessageHandler += (x, y, z) => exposedMessages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));

            //Act
            SetLevelEnabled(logger, level, false);
            WriteAllOverloads(logger, level);

            //Assert
            Assert.IsFalse(GetLevelEnabled(logger, level), level + " should be reported as disabled after the assignment.");
            Assert.AreEqual(0, writtenMessages.Count, level + " overloads must not write while the level is disabled.");
            Assert.AreEqual(0, exposedMessages.Count, level + " overloads must not expose messages while the level is disabled.");
        }

        [Test]
        public void Logger_LevelSetToTrue_MessagesAreWrittenAndExposed([ValueSource(nameof(Levels))] LogLevel level)
        {
            //Arrange
            var logger = CreateLogger(false);
            logger.LogMessageHandler += (x, y, z) => exposedMessages.Add(new Tuple<LogLevel, string, Exception>(x, y, z));

            //Act
            SetLevelEnabled(logger, level, true);
            WriteAllOverloads(logger, level);

            //Assert
            Assert.IsTrue(GetLevelEnabled(logger, level), level + " should be reported as enabled after the assignment.");
            Assert.AreEqual(3, writtenMessages.Count, "All " + level + " overloads must write while the level is enabled.");
            Assert.AreEqual(3, exposedMessages.Count, "All " + level + " overloads must expose messages while the level is enabled.");
        }

        [Test]
        public void Logger_LevelNotAssigned_FollowsTheUnderlyingLogMethods([ValueSource(nameof(Levels))] LogLevel level)
        {
            //Arrange
            var enabledLogger = CreateLogger(true);
            var disabledLogger = CreateLogger(false);

            //Assert
            Assert.IsTrue(GetLevelEnabled(enabledLogger, level), level + " should follow an enabled ILogMethods while unassigned.");
            Assert.IsFalse(GetLevelEnabled(disabledLogger, level), level + " should follow a disabled ILogMethods while unassigned.");
        }

        [Test]
        public void Logger_LevelDisabledThenReEnabled_MessagesAreWrittenAgain()
        {
            //Arrange
            var logger = CreateLogger(true);

            //Act
            logger.IsInfoEnabled = false;
            logger.Info("suppressed");
            logger.IsInfoEnabled = true;
            logger.Info("written");

            //Assert
            Assert.AreEqual(1, writtenMessages.Count, "Only the message written while info was enabled must reach the sink.");
            Assert.AreEqual("written", writtenMessages[0], "The message written while info was disabled must not reach the sink.");
        }

        [Test]
        public void Logger_OneLevelDisabled_OtherLevelsAreUnaffected()
        {
            //Arrange
            var logger = CreateLogger(true);

            //Act
            logger.IsInfoEnabled = false;
            logger.Info("suppressed");
            logger.Debug("debug");
            logger.Warn("warn");

            //Assert
            Assert.IsFalse(logger.IsInfoEnabled, "Info should be reported as disabled.");
            Assert.IsTrue(logger.IsDebugEnabled, "Debug must stay enabled.");
            Assert.IsTrue(logger.IsWarnEnabled, "Warn must stay enabled.");
            CollectionAssert.AreEqual(new[] { "debug", "warn" }, writtenMessages, "Only the disabled level must be suppressed.");
        }
    }
}
