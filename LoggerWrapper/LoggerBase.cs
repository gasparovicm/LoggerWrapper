
using System;
using System.Globalization;


namespace idev.LoggerWrapper
{
    public abstract class LoggerBase : ILogger, ILoggerWithMessagesExpositionWithLevels
    {

        #region Fields

        private bool isFatalEnabled = true;
        private bool isErrorEnabled = true;
        private bool isWarnEnabled = true;
        private bool isTraceEnabled = true;
        private bool isDebugEnabled = true;
        private bool isInfoEnabled = true;

        #endregion

        protected void InvokeLogMessageEvent(LogLevel level, string message, Exception ex)
        {
            if (LogMessageHandler != null) LogMessageHandler.Invoke(level, message, ex);
        }

        #region Public methods

        public virtual void FatalFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsFatalEnabled) Fatal(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Fatal(string message)
        {
            if (IsFatalEnabled) InvokeLogMessageEvent(LogLevel.Fatal, message, null);
        }

        public virtual void Fatal(Exception ex)
        {
            if (IsFatalEnabled) InvokeLogMessageEvent(LogLevel.Fatal, string.Empty, ex);
        }

        public virtual void Fatal(string message, Exception ex)
        {
            if (IsFatalEnabled) InvokeLogMessageEvent(LogLevel.Fatal, message, ex);
        }

        public virtual void ErrorFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsErrorEnabled) Error(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Error(string message)
        {
            if (IsErrorEnabled) InvokeLogMessageEvent(LogLevel.Error, message, null);
        }

        public virtual void Error(Exception ex)
        {
            if (IsErrorEnabled) InvokeLogMessageEvent(LogLevel.Error, string.Empty, ex);
        }

        public virtual void Error(string message, Exception ex)
        {
            if (IsErrorEnabled) InvokeLogMessageEvent(LogLevel.Error, message, ex);
        }

        public virtual void WarnFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsWarnEnabled) Warn(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Warn(string message)
        {
            if (IsWarnEnabled) InvokeLogMessageEvent(LogLevel.Warn, message, null);
        }

        public virtual void Warn(Exception ex)
        {
            if (IsWarnEnabled) InvokeLogMessageEvent(LogLevel.Warn, string.Empty, ex);
        }

        public virtual void Warn(string message, Exception ex)
        {
            if (IsWarnEnabled) InvokeLogMessageEvent(LogLevel.Warn, message, ex);
        }

        public virtual void TraceFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsTraceEnabled) Trace(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Trace(string message)
        {
            if (IsTraceEnabled) InvokeLogMessageEvent(LogLevel.Trace, message, null);
        }

        public virtual void Trace(Exception ex)
        {
            if (IsTraceEnabled) InvokeLogMessageEvent(LogLevel.Trace, string.Empty, ex);
        }

        public virtual void Trace(string message, Exception ex)
        {
            if (IsTraceEnabled) InvokeLogMessageEvent(LogLevel.Trace, message, ex);
        }

        public virtual void DebugFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsDebugEnabled) Debug(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Debug(string message)
        {
            if (IsDebugEnabled) InvokeLogMessageEvent(LogLevel.Debug, message, null);
        }

        public virtual void Debug(Exception ex)
        {
            if (IsDebugEnabled) InvokeLogMessageEvent(LogLevel.Debug, string.Empty, ex);
        }

        public virtual void Debug(string message, Exception ex)
        {
            if (IsDebugEnabled) InvokeLogMessageEvent(LogLevel.Debug, message, ex);
        }

        public virtual void InfoFormat(CultureInfo culture, string message, params object[] objects)
        {
            if (IsInfoEnabled) Info(string.Format(culture ?? CultureInfo.InvariantCulture, message, objects));
        }

        public virtual void Info(string message)
        {
            if (IsInfoEnabled) InvokeLogMessageEvent(LogLevel.Info, message, null);
        }

        public virtual void Info(Exception ex)
        {
            if (IsInfoEnabled) InvokeLogMessageEvent(LogLevel.Info, string.Empty, ex);
        }

        public virtual void Info(string message, Exception ex)
        {
            if (IsInfoEnabled) InvokeLogMessageEvent(LogLevel.Info, message, ex);
        }


        #endregion

        #region Public properties
        public virtual event Action<LogLevel, string, Exception> LogMessageHandler;

        public virtual bool IsFatalEnabled
        {
            get { return isFatalEnabled; }
            set { isFatalEnabled = value; }
        }

        public virtual bool IsErrorEnabled
        {
            get { return isErrorEnabled; }
            set { isErrorEnabled = value; }
        }

        public virtual bool IsWarnEnabled
        {
            get { return isWarnEnabled; }
            set { isWarnEnabled = value; }
        }

        public virtual bool IsTraceEnabled
        {
            get { return isTraceEnabled; }
            set { isTraceEnabled = value; }
        }

        public virtual bool IsDebugEnabled
        {
            get { return isDebugEnabled; }
            set { isDebugEnabled = value; }
        }

        public virtual bool IsInfoEnabled
        {
            get { return isInfoEnabled; }
            set { isInfoEnabled = value; }
        }
        #endregion


    }
}

