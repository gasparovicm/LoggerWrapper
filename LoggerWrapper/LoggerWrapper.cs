using System;
using System.Linq;
using System.Reflection;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Purpose of this class is to enable injection of external logger to ClientCoreContex classes
    /// </summary>
    public class LoggerWrapper : LoggerBase
    {
        #region Public properties

        public override bool IsFatalEnabled
        {
            get { return fatalLog.IsEnabled; }
        }

        public override bool IsErrorEnabled
        {
            get { return errorLog.IsEnabled; }
        }

        public override bool IsWarnEnabled
        {
            get { return warningLog.IsEnabled; }
        }

        public override bool IsTraceEnabled
        {
            get { return traceLog.IsEnabled; }
        }

        public override bool IsDebugEnabled
        {
            get { return debugLog.IsEnabled; }
        }

        public override bool IsInfoEnabled
        {
            get { return infoLog.IsEnabled; }
        }
        #endregion

        #region Fields
        private readonly ILogMethods fatalLog;
        private readonly ILogMethods errorLog;
        private readonly ILogMethods warningLog;
        private readonly ILogMethods traceLog;
        private readonly ILogMethods debugLog;
        private readonly ILogMethods infoLog;

        #endregion

        #region Constructors


        /// <summary>
        /// All messages will have appropriate prefix . "Fatal: ", "Error: ","Warn: ","Trace: ","Debug: ","Info: "
        /// </summary>
        /// <param name="log"></param>
        /// <param name="name"></param>
        public LoggerWrapper(ILogMethods log,string name)
        {
            infoLog = new LogMethodsWithPrefix(log.Write,log.Write, log.Write, name, LogLevel.Info);
            debugLog = new LogMethodsWithPrefix(log.Write, log.Write, log.Write, name, LogLevel.Debug);
            traceLog = new LogMethodsWithPrefix(log.Write, log.Write, log.Write, name, LogLevel.Trace);
            warningLog = new LogMethodsWithPrefix(log.Write, log.Write, log.Write, name, LogLevel.Warn);
            errorLog = new LogMethodsWithPrefix(log.Write, log.Write, log.Write, name, LogLevel.Error);
            fatalLog = new LogMethodsWithPrefix(log.Write, log.Write, log.Write, name, LogLevel.Fatal);
        }

        public LoggerWrapper( ILogMethods fatalLog, ILogMethods errorLog,
            ILogMethods warningLog, ILogMethods traceLog, ILogMethods debugLog, ILogMethods infoLog)
        {
            this.infoLog = infoLog ?? EmptyLogMethods.Instance;
            this.debugLog = debugLog ?? EmptyLogMethods.Instance;
            this.traceLog = traceLog ?? EmptyLogMethods.Instance;
            this.warningLog = warningLog ?? EmptyLogMethods.Instance;
            this.errorLog = errorLog ?? EmptyLogMethods.Instance;
            this.fatalLog = fatalLog ?? EmptyLogMethods.Instance;
        }
        #endregion Constructors

        #region Private methods

        private static ILogMethods CreateLogMethods(MethodInfo[] methods, Object log4Net, String methodName)
        {
            var writeMethodInfo = GetWriteMessageMethodInfo(methods, methodName);
            if (writeMethodInfo==null) throw new Exception("Can't access log method " +"\"" +methodName+"\" with parameter (message).");
            var writeExceptionMethodInfo = GetWriteExceptionMethodInfo(methods, methodName);
            if (writeExceptionMethodInfo == null) throw new Exception("Can't access log method " + "\"" + methodName + "\" with parameter (Exception).");
            var writeWithExceptionMethodInfo = GetWriteMessageWithExceptionMethodInfo(methods,  methodName);
            if (writeWithExceptionMethodInfo == null) throw new Exception("Can't access log method " + "\"" + methodName + "\" with parameter (message,Exception).");

            try
            {
                return LogMethods.CreateLogMethodsFromObjectMethods(GetWriteMessageMethod(writeMethodInfo, log4Net)
                    ,GetWriteExceptionMethod(writeExceptionMethodInfo, log4Net)
                    , GetWriteMessageWithExceptionMethod(writeWithExceptionMethodInfo, log4Net));
            }
            catch (ArgumentException)
            {
                return new LogMethodsFromMethodInfos(log4Net, writeMethodInfo, writeExceptionMethodInfo,writeWithExceptionMethodInfo);
            }

        }

        private static LogMethods.WriteObject GetWriteMessageMethod(MethodInfo methodInfo, Object instance)
        {
            LogMethods.WriteObject method = null;
            if (methodInfo != null)
            {
                method = (LogMethods.WriteObject) Delegate.CreateDelegate(typeof (LogMethods.WriteObject), instance, methodInfo);
            }

            return method;
        }

        private static MethodInfo GetWriteMessageMethodInfo(MethodInfo[] methods, String methodName)
        {
            var specificMethods = from mi in methods
                                  let p = mi.GetParameters()
                                  where p.Length == 1
                                        && (p[0].ParameterType == typeof(object) || p[0].ParameterType == typeof(string))
                                        && mi.Name == methodName
                                        && mi.IsStatic == false
                                        && mi.IsPublic
                                  select mi;
            var specificMethodsArray = specificMethods.ToArray();
            if (specificMethodsArray.Count() == 1)
            {
                return specificMethodsArray[0];
            }
            return null;
        }

        private static LogMethods.WriteObjectException GetWriteExceptionMethod(MethodInfo methodInfo, Object instance)
        {
            LogMethods.WriteObjectException method = null;
            if (methodInfo != null)
            {
                method = (LogMethods.WriteObjectException)Delegate.CreateDelegate(typeof(LogMethods.WriteObjectException),
                    instance, methodInfo);
            }
            return method;
        }

        private static LogMethods.WriteObjectWithException GetWriteMessageWithExceptionMethod(MethodInfo methodInfo, Object instance)
        {
            LogMethods.WriteObjectWithException method = null;
            if (methodInfo != null)
            {
                method = (LogMethods.WriteObjectWithException)Delegate.CreateDelegate(typeof(LogMethods.WriteObjectWithException),
                    instance, methodInfo);
            }
            return method;
        }

        private static MethodInfo GetWriteExceptionMethodInfo(MethodInfo[] methods, String methodName)
        {
            var specificMethods = from mi in methods
                                  let p = mi.GetParameters()
                                  where p.Length == 1
                                        && p[0].ParameterType == typeof(Exception)
                                        && mi.Name == methodName
                                        && mi.IsPublic
                                  select mi;
            var specificMethodsArray = specificMethods.ToArray();
            if (specificMethodsArray.Count() == 1)
            {
                return specificMethodsArray[0];
            }

            specificMethods = from mi in methods
                              let p = mi.GetParameters()
                              where p.Length == 1
                                    && p[0].ParameterType == typeof(object)
                                    && mi.Name == methodName
                                    && mi.IsPublic
                              select mi;
            specificMethodsArray = specificMethods.ToArray();
            if (specificMethodsArray.Count() == 1)
            {
                return specificMethodsArray[0];
            }
            return null;
        }

        private static MethodInfo GetWriteMessageWithExceptionMethodInfo(MethodInfo[] methods, String methodName)
        {
            var specificMethods = from mi in methods
                                  let p = mi.GetParameters()
                                  where p.Length == 2
                                        && (p[0].ParameterType == typeof(object) || p[0].ParameterType == typeof(string))
                                        && p[1].ParameterType == typeof(Exception)
                                        && mi.Name == methodName
                                        && mi.IsPublic
                                  select mi;
            var specificMethodsArray = specificMethods.ToArray();
            if (specificMethodsArray.Count() == 1)
            {
                return specificMethodsArray[0];
            }
            return null;
        }
        #endregion

        #region Public methods

        public static LoggerWrapper CreateFromILogLikeObject(Object logObject)
        {
            //var logger = (ILog) log4Net;
            //if (logger != null)
            //{
            //    return new Logger(
            //         new LogMethods(logger.Fatal, logger.Fatal),
            //         new LogMethods(logger.Error, logger.Error),
            //         new LogMethods(logger.Warn, logger.Warn),
            //         new LogMethods(logger.Debug, logger.Debug),
            //         new LogMethods(logger.Trace, logger.Trace),
            //         new LogMethods(logger.Info, logger.Info)
            //         );
            //}

            var log4NetType = logObject.GetType();
            var methods = log4NetType.GetMethods();
            var fatalLog = CreateLogMethods(methods, logObject, "Fatal");
            var errorLog = CreateLogMethods(methods, logObject, "Error");
            var warnLog = CreateLogMethods(methods, logObject, "Warn");
            var debugLog = CreateLogMethods(methods, logObject, "Debug");
            var traceLog = CreateLogMethods(methods, logObject, "Trace");
            var infoLog = CreateLogMethods(methods, logObject, "Info");

            return new LoggerWrapper(fatalLog, errorLog, warnLog,  traceLog, debugLog, infoLog);
        }

        public override void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                fatalLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Fatal, message,null);
            }
        }

        public override void Fatal(Exception ex)
        {
            if (IsFatalEnabled)
            {
                fatalLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Fatal, string.Empty, ex);
            }
        }

        public override void Fatal(string message, Exception ex)
        {
            if (IsFatalEnabled)
            {
                fatalLog.Write(message, ex);
                InvokeLogMessageEvent(LogLevel.Fatal, message, ex);
            }
        }

        public override void Error(string message)
        {
            if (IsErrorEnabled)
            {
                errorLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Error, message, null);
            }
        }

        public override void Error(Exception ex)
        {
            if (IsErrorEnabled)
            {
                errorLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Error, string.Empty, ex);
            }
        }

        public override void Error(string message, Exception ex)
        {
            if (IsErrorEnabled)
            {
                errorLog.Write(message, ex);
                InvokeLogMessageEvent(LogLevel.Error, message, ex);
            }
        }
        
        public override void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                warningLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Warn, message, null);
            }
        }

        public override void Warn( Exception ex)
        {
            if (IsWarnEnabled)
            {
                warningLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Warn, string.Empty, ex);
            }
        }

        public override void Warn(string message, Exception ex)
        {
            if (IsWarnEnabled)
            {
                warningLog.Write(message, ex);
                InvokeLogMessageEvent(LogLevel.Warn, message, ex);
            }
        }

        public override void Trace(string message)
        {
            if (IsDebugEnabled)
            {
                traceLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Trace, message, null);
            }
        }

        public override void Trace( Exception ex)
        {
            if (IsDebugEnabled)
            {
                traceLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Trace, string.Empty, ex);
            }
        }

        public override void Trace(string message, Exception ex)
        {
            if (IsDebugEnabled)
            {
                traceLog.Write(message, ex);
                InvokeLogMessageEvent(LogLevel.Trace, message, ex);
            }
        }

        public override void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                debugLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Debug, message, null);
            }
        }

        public override void Debug(Exception ex)
        {
            if (IsDebugEnabled)
            {
                debugLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Debug, string.Empty, ex);
            }
        }

        public override void Debug(string message, Exception ex)
        {
            if (IsDebugEnabled)
            {
                debugLog.Write(message,ex);
                InvokeLogMessageEvent(LogLevel.Debug, message, ex);
            }
        }

        public override void Info(string message)
        {
            if (IsInfoEnabled)
            {
                infoLog.Write(message);
                InvokeLogMessageEvent(LogLevel.Info, message, null);
            }
        }

        public override void Info(Exception ex)
        {
            if (IsInfoEnabled)
            {
                infoLog.Write(ex);
                InvokeLogMessageEvent(LogLevel.Info, string.Empty, ex);
            }
        }

        public override void Info(string message, Exception ex)
        {
            if (IsInfoEnabled)
            {
                infoLog.Write(message, ex);
                InvokeLogMessageEvent(LogLevel.Info, message, ex);
            }
        }
        #endregion

    }
}
