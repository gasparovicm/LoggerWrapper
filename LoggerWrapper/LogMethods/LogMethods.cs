using System;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Implementation of ILogMethods init interface based on delegates
    /// </summary>
    public class LogMethods : ILogMethods
    {
        public delegate void WriteMessage(string message);
        public delegate void WriteException(Exception ex);
        public delegate void WriteMessageWithException(string message, Exception ex);
        public delegate void WriteObject(object message);
        public delegate void WriteObjectException(Exception ex);
        public delegate void WriteObjectWithException(object message, Exception ex);

        protected readonly WriteMessage writeMessage;
        protected readonly WriteException writeException;
        protected readonly WriteMessageWithException writeMessageWithException;

        public LogMethods( WriteMessage writeMessage, WriteException writeException, WriteMessageWithException writeMessageWithException, bool isEnabled=true, ILoggerWithMessagesExpositionWithLevels exposeMessageMethod=null)
        {
            IsEnabled = isEnabled;
            this.writeMessage = writeMessage;
            this.writeException = writeException;
            this.writeMessageWithException = writeMessageWithException;
        }

        public static LogMethods CreateLogMethodsFromObjectMethods(WriteObject writeMessage, WriteObjectException writeObjectException, WriteObjectWithException writeMessageWithException)
        {
            return new LogMethods(x => writeMessage(x), (x) => writeObjectException(x),(x, y) => writeMessageWithException(x, y));
        }

        public virtual void Write(string message)
        {
            writeMessage(message);
        }

        public virtual void Write( Exception ex)
        {
            writeException(ex);
        }

        public virtual void Write(string message, Exception ex)
        {
            writeMessageWithException(message, ex);
        }

        private bool isEnabled = true;
        public bool IsEnabled { get { return isEnabled; } private set { isEnabled = value; } }
    }
}
