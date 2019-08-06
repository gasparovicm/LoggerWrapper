using System;
using System.Linq;
using System.Reflection;
using Common.Logging;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Purpose of this class is to enable injection of external logger to ClientCoreContex classes
    /// </summary>
    public class LoggerWrapperCommonLog : LoggerWrapper
    {


        #region Constructors

        public LoggerWrapperCommonLog(ILog logger)
            : base(new LogMethods(logger.Fatal, logger.Fatal, logger.Fatal, logger.IsFatalEnabled),
            new LogMethods(logger.Error, logger.Error, logger.Error, logger.IsErrorEnabled),
            new LogMethods(logger.Warn, logger.Warn, logger.Warn, logger.IsWarnEnabled),
            new LogMethods(logger.Trace, logger.Trace, logger.Trace, logger.IsTraceEnabled),
            new LogMethods(logger.Debug, logger.Debug, logger.Debug, logger.IsDebugEnabled),
            new LogMethods(logger.Info, logger.Info, logger.Info, logger.IsInfoEnabled))
        {

        }

        #endregion Constructors


        #region Public methods


        public static LoggerWrapperCommonLog CreateFromLog4Net(ILog log4Net)
        {
            if (log4Net != null)
            {
                return new LoggerWrapperCommonLog(log4Net);
            }
            throw new ArgumentNullException("log4Net");
        }

        #endregion

    }
}
