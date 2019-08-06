using System;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Log4Net;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Default implementation of ILogger for ClientCoreContext, based on Common.Logging and log4Net
    /// </summary>
    public class Log4NetLogger : LoggerBase
    {
        #region Fields
        

        private readonly ILog logger;
        #endregion

        #region Constructors
        static Log4NetLogger()
        {
            global::Common.Logging.LogManager.Adapter = new global::Common.Logging.Log4Net.Universal.Log4NetFactoryAdapter(new NameValueCollection());
            log4net.Config.XmlConfigurator.Configure();
        }

        public Log4NetLogger(Type type)
            : this(global::Common.Logging.LogManager.GetLogger(type))
        {
        }

        public Log4NetLogger(string name)
            : this(global::Common.Logging.LogManager.GetLogger(name))
        {

        }

        internal Log4NetLogger(ILog logger)            
        {
            this.logger = logger;
        }

        #endregion Constructors

        #region Public methods

        public override void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                logger.Fatal(message);
                InvokeLogMessageEvent(LogLevel.Fatal, message, null);
            }
        }

        public override void Fatal(Exception ex)
        {
            if (IsFatalEnabled)
            {
                logger.Fatal(ex);
                InvokeLogMessageEvent(LogLevel.Fatal, string.Empty, ex);
            }
        }

        public override void Fatal(string message, Exception ex)
        {
            if (IsFatalEnabled)
            {
                logger.Fatal(message, ex);
                InvokeLogMessageEvent(LogLevel.Fatal, message, ex);
            }
        }

        public override void Error(string message)
        {
            if (IsErrorEnabled)
            {
                logger.Error(message);
                InvokeLogMessageEvent(LogLevel.Error, message, null);
            }
        }

        public override void Error(Exception ex)
        {
            if (IsErrorEnabled)
            {
                logger.Error(ex);
                InvokeLogMessageEvent(LogLevel.Error, string.Empty, ex);
            }
        }

        public override void Error(string message, Exception ex)
        {
            if (IsErrorEnabled)
            {
                logger.Error(message, ex);
                InvokeLogMessageEvent(LogLevel.Error, message, ex);
            }
        }

        public override void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                logger.Warn(message);
                InvokeLogMessageEvent(LogLevel.Warn, message, null);
            }
        }

        public override void Warn(Exception ex)
        {
            if (IsWarnEnabled)
            {
                logger.Warn(ex);
                InvokeLogMessageEvent(LogLevel.Warn, string.Empty, ex);
            }
        }

        public override void Warn(string message, Exception ex)
        {
            if (IsWarnEnabled)
            {
                logger.Warn(message, ex);
                InvokeLogMessageEvent(LogLevel.Warn, message, ex);
            }
        }

        public override void Trace(string message)
        {
            if (IsTraceEnabled)
            {
                logger.Trace(message);
                InvokeLogMessageEvent(LogLevel.Trace, message, null);
            }
        }

        public override void Trace(Exception ex)
        {
            if (IsTraceEnabled)
            {
                logger.Trace(ex);
                InvokeLogMessageEvent(LogLevel.Trace, string.Empty, ex);
            }
        }

        public override void Trace(string message, Exception ex)
        {
            if (IsTraceEnabled)
            {
                logger.Trace(message, ex);
                InvokeLogMessageEvent(LogLevel.Trace, message, ex);
            }
        }

        public override void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(message);
                InvokeLogMessageEvent(LogLevel.Debug, message, null);
            }
        }

        public override void Debug(Exception ex)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(ex);
                InvokeLogMessageEvent(LogLevel.Debug, string.Empty, ex);
            }
        }

        public override void Debug(string message, Exception ex)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(message, ex);
                InvokeLogMessageEvent(LogLevel.Debug, message, ex);
            }
        }
        
        public override void Info(string message)
        {
            if (IsInfoEnabled)
            {
                logger.Info(message);
                InvokeLogMessageEvent(LogLevel.Info, message, null);
            }
        }

        public override void Info(Exception ex)
        {
            if (IsInfoEnabled)
            {
                logger.Info(ex);
                InvokeLogMessageEvent(LogLevel.Info, string.Empty, ex);
            }
        }

        public override void Info(string message, Exception ex)
        {
            if (IsInfoEnabled)
            {
                logger.Info(message, ex);
                InvokeLogMessageEvent(LogLevel.Info, message, ex);
            }
        }
        #endregion
        //Todo:parameters from log4net
        #region Public properties

        public override bool IsFatalEnabled
        {
            get { return logger.IsFatalEnabled; }
        }

        public override bool IsErrorEnabled
        {
            get { return logger.IsErrorEnabled; }
        }

        public override bool IsWarnEnabled
        {
            get { return logger.IsWarnEnabled; }
        }

        public override bool IsTraceEnabled
        {
            get { return logger.IsTraceEnabled; }
        }

        public override bool IsDebugEnabled
        {
            get { return logger.IsDebugEnabled; }
        }

        public override bool IsInfoEnabled
        {
            get { return logger.IsInfoEnabled; }
        }
        #endregion
    }
}
