using System;
using System.Globalization;

namespace idev.LoggerWrapper
{

    public class LoggerWithLevels : ILoggerWithLevels
    {
        public event Action<LogLevel, string, Exception> LogMessageHandler;
        private LogLevel logLevel;
        private ILoggerBasic logger;

        public LogLevel LogLevel
        {
            get
            {
                return logLevel;
            }

            set
            {
                logLevel = value;
            }
        }

        public ILoggerBasic Logger
        {
            get
            {
                return logger;
            }

            set
            {
                logger = value;
            }
        }
        

        public LoggerWithLevels( ILoggerBasic logger,LogLevel logLevel = LogLevel.Info )//[System.Runtime.CompilerServices.CallerMemberName] string memberName = ""
        {
            this.logLevel = logLevel;
            if (logger == null) throw new ArgumentNullException(paramName: "logger");
            this.logger = logger;
        }

        public void Log(LogLevel logLevel ,string message)
        {
            if (logLevel < this.logLevel) return;
            if (LogMessageHandler != null) LogMessageHandler.Invoke(logLevel,message,null);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    logger.Trace(message);
                    break;
                case LogLevel.Debug:
                    logger.Debug(message);
                    break;
                case LogLevel.Info:
                    logger.Info(message);
                    break;
                case LogLevel.Warn:
                    logger.Warn(message);
                    break;
                case LogLevel.Error:
                    logger.Error(message);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(message);
                    break;
                default:
                    logger.Error(string.Format(CultureInfo.InvariantCulture, "Log level not recognized: {0}. Message: {1}:", logLevel, message));
                    break;
            }
        }

        public void Log(LogLevel logLevel, Exception ex)
        {
            if (logLevel < this.logLevel) return;
            if (LogMessageHandler != null) LogMessageHandler.Invoke(logLevel, null, ex);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    logger.Trace(ex);
                    break;
                case LogLevel.Debug:
                    logger.Debug(ex);
                    break;
                case LogLevel.Info:
                    logger.Info(ex);
                    break;
                case LogLevel.Warn:
                    logger.Warn(ex);
                    break;
                case LogLevel.Error:
                    logger.Error(ex);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(ex);
                    break;
                default:
                    logger.Error(string.Format(CultureInfo.InvariantCulture, "Log level not recognized: {0}. Exception: {1}:", logLevel, ex.Message));
                    break;
            }
        }

        public void Log(LogLevel logLevel, string message, Exception ex)
        {
            if (logLevel < this.logLevel) return;
            if (LogMessageHandler != null) LogMessageHandler.Invoke(logLevel, message, ex);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    logger.Trace(message,ex);
                    break;
                case LogLevel.Debug:
                    logger.Debug(message,ex);
                    break;
                case LogLevel.Info:
                    logger.Info(message,ex);
                    break;
                case LogLevel.Warn:
                    logger.Warn(message,ex);
                    break;
                case LogLevel.Error:
                    logger.Error(message,ex);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(message,ex);
                    break;
                default:
                    logger.Error(string.Format(CultureInfo.InvariantCulture, "Log level not recognized: {0}. Message: {1} . Exception: {2}:", logLevel,message, ex.Message));
                    break;
            }
        }

        public void LogFormat(LogLevel logLevel, CultureInfo culture, string message, params object[] objects)
        {
            if (logLevel < this.logLevel) return;
            string formattedMessage = string.Format(culture ?? CultureInfo.InvariantCulture, message, objects);
            Log(logLevel,formattedMessage);
        }

    }
}
