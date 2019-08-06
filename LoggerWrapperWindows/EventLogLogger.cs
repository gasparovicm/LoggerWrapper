using System;
using System.Diagnostics;

namespace idev.LoggerWrapper.Windows
{
    public class EventLogLogger : LoggerBase
    {
        private string name;
        private string appName;
        public Action<string, string, EventLogEntryType> WriteEntryToEventLog = EventLog.WriteEntry;

        public EventLogLogger(string logName) : this(System.AppDomain.CurrentDomain.FriendlyName, logName)
        {
        }

        public EventLogLogger(string appName, string logName)
        {
            name = logName;
            this.appName = appName;
        }
        #region Public methods

        public override void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Fatal, message, null);
                WriteEventLogMessage("Fatal:" + Environment.NewLine + message, EventLogEntryType.Error);
            }
        }

        public override void Fatal(Exception ex)
        {
            if (IsFatalEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Fatal, string.Empty, ex);
                WriteEventLogMessage("Fatal:" + Environment.NewLine + ex, EventLogEntryType.Error);
            }
        }

        public override void Fatal(string message, Exception ex)
        {
            if (IsFatalEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Fatal, message, ex);
                WriteEventLogMessage("Fatal:" + Environment.NewLine + message + Environment.NewLine + ex, EventLogEntryType.Error);

            }
        }

        public override void Error(string message)
        {
            if (IsErrorEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Error, message, null);
                WriteEventLogMessage(message, EventLogEntryType.Error);
            }
        }

        public override void Error(Exception ex)
        {
            if (IsErrorEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Error, string.Empty, ex);
                WriteEventLogMessage(ex.ToString(), EventLogEntryType.Error);
            }
        }

        public override void Error(string message, Exception ex)
        {
            if (IsErrorEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Error, message, ex);
                WriteEventLogMessage(message + Environment.NewLine + ex, EventLogEntryType.Error);
            }
        }


        public override void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Warn, message, null);
                WriteEventLogMessage(message, EventLogEntryType.Warning);
            }
        }

        public override void Warn(Exception ex)
        {
            if (IsWarnEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Warn, string.Empty, ex);
                WriteEventLogMessage(ex.ToString(), EventLogEntryType.Warning);
            }
        }

        public override void Warn(string message, Exception ex)
        {
            if (IsWarnEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Warn, message, ex);
                WriteEventLogMessage(message + Environment.NewLine + ex, EventLogEntryType.Warning);
            }
        }

        public override void Trace(string message)
        {
            if (IsTraceEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Trace, message, null);
                WriteEventLogMessage("Trace:" + Environment.NewLine + message, EventLogEntryType.Information);
            }
        }

        public override void Trace(Exception ex)
        {
            if (IsTraceEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Trace, string.Empty, ex);
                WriteEventLogMessage("Trace:" + Environment.NewLine + ex, EventLogEntryType.Information);
            }
        }

        public override void Trace(string message, Exception ex)
        {
            if (IsTraceEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Trace, message, ex);
                WriteEventLogMessage("Trace:" + Environment.NewLine + message + Environment.NewLine + ex, EventLogEntryType.Information);

            }
        }

        public override void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Debug, message, null);
                WriteEventLogMessage("Debug:" + Environment.NewLine + message, EventLogEntryType.Information);
            }
        }

        public override void Debug(Exception ex)
        {
            if (IsDebugEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Debug, string.Empty, ex);
                WriteEventLogMessage("Debug:" + Environment.NewLine + ex, EventLogEntryType.Information);
            }
        }

        public override void Debug(string message, Exception ex)
        {
            if (IsDebugEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Debug, message, ex);
                WriteEventLogMessage("Debug:" + Environment.NewLine + message + Environment.NewLine + ex, EventLogEntryType.Information);
            }
        }

        public override void Info(string message)
        {
            if (IsInfoEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Info, message, null);
                WriteEventLogMessage(message, EventLogEntryType.Information);
            }
        }

        public override void Info(Exception ex)
        {
            if (IsInfoEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Info, string.Empty, ex);
                WriteEventLogMessage(ex.ToString(), EventLogEntryType.Information);
            }
        }

        public override void Info(string message, Exception ex)
        {
            if (IsInfoEnabled)
            {
                InvokeLogMessageEvent(LogLevel.Info, message, ex);
                WriteEventLogMessage(message + Environment.NewLine + ex, EventLogEntryType.Information);
            }
        }

        #endregion public methods

        private void WriteEventLogMessage(string message, EventLogEntryType logMessageEntryType)
        {
            WriteEntryToEventLog(appName, name + Environment.NewLine + message, logMessageEntryType);
        }

    }
}