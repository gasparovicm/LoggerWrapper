using System;

namespace idev.LoggerWrapper
{

    public interface ILoggerWithMessagesExpositionWithLevels
    {
        event Action<LogLevel,string,Exception> LogMessageHandler;
    }
}
