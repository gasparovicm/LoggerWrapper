using System;

namespace idev.LoggerWrapper
{
    public interface ILoggerBasic
    {
        void Fatal(string message);
        void Fatal(string message, Exception ex);
        void Fatal(Exception ex);

        
        void Error(string message);
        void Error(string message, Exception ex);
        void Error(Exception ex);

        void Warn(string message);
        void Warn(string message, Exception ex);
        void Warn(Exception ex);

        void Trace(string message);
        void Trace(string message, Exception ex);
        void Trace(Exception ex);

        void Debug(string message);
        void Debug(string message, Exception ex);
        void Debug(Exception ex);

        void Info(string message);
        void Info(string message, Exception ex);
        void Info(Exception ex);

        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }

    }
}
