using System;

namespace idev.LoggerWrapper
{
    public interface ILoggerWithLevels : ILoggerWithMessagesExpositionWithLevels
    {

        LogLevel LogLevel { get; set; }
        void Log(LogLevel logLevel, string message);
        void Log(LogLevel logLevel, Exception ex);
        void Log(LogLevel logLevel, string message, Exception ex);
    }
}