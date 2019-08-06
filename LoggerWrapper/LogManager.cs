using System;
using System.Linq;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Simple factory for default logger.
    /// </summary>
    public static class LogManager
    {
        private static Type loggerType = typeof(NoOpLogger);

        public static void SetLoggerType(Type type) 
        {
            if (!type.GetInterfaces().Contains(typeof(ILogger)))
            {
                throw new ArgumentException("ILogger interface not implemented", paramName: "type");
            }
            loggerType = type;
        }
        public static ILogger GetLogger(Type type)
        {
            return (ILogger)Activator.CreateInstance(loggerType, type);            
        }

        public static ILogger GetLogger(string name)
        {
            return (ILogger)Activator.CreateInstance(loggerType, name);           
        }

        public static ILoggerWithLevels GetLoggerWithLevels(string name,LogLevel level = LogLevel.Info)
        {
            return new LoggerWithLevels(GetLogger(name),level);
        }

        public static ILoggerWithLevels GetLoggerWithLevels(Type type, LogLevel level = LogLevel.Info)
        {
            return new LoggerWithLevels(GetLogger(type), level);
        }

    }
}
