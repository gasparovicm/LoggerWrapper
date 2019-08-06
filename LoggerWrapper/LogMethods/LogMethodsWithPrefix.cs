using System;
using System.Threading;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Implementation of ILogMethods init interface based on delegates
    /// </summary>
    internal class LogMethodsWithPrefix : LogMethods
    {
        private readonly LogLevel logLevel;
        private readonly string name;


        public LogMethodsWithPrefix(WriteMessage writeMessage, WriteException writeException, WriteMessageWithException writeMessageWithException, string name , LogLevel logLevel )
            : base( writeMessage, writeException, writeMessageWithException, true)
        {
            this.logLevel = logLevel;
            this.name = name;
        }

        public override void Write(string message)
        {
            writeMessage(GetStandardmessagePrefix() + message);
        }

        public override void Write(Exception ex)
        {
            writeMessageWithException(GetStandardmessagePrefix(), ex);
        }

        public override void Write(string message, Exception ex)
        {
            writeMessageWithException(GetStandardmessagePrefix() + message, ex);
        }

        private string GetStandardmessagePrefix()
        {
            var prefix = DateTime.Today + " [" + Thread.CurrentThread.Name + "] " + (string.IsNullOrEmpty(name) ? string.Empty : name) + " " + logLevel;
            return prefix;
        }
    }
}
