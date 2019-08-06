using System;
using System.Reflection;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Implementation of ILogMethods init interface based on MethodInfo
    /// </summary>
    public class LogMethodsFromMethodInfos : ILogMethods
    {

        private readonly Object invokeObject;
        private readonly MethodInfo writeMessageInfo;
        private readonly MethodInfo writeExceptionInfo;
        private readonly MethodInfo writeMessageWithExceptionInfo;

        public LogMethodsFromMethodInfos(Object invokeObject, MethodInfo writeMessageInfo, MethodInfo writeExceptionInfo, MethodInfo writeMessageWithExceptionInfo)
        {
            this.invokeObject = invokeObject;
            this.writeMessageInfo = writeMessageInfo;
            this.writeExceptionInfo = writeExceptionInfo;
            this.writeMessageWithExceptionInfo = writeMessageWithExceptionInfo;
        }

        public void Write(string message)
        {
            writeMessageInfo.Invoke(invokeObject, new object[] { message });
        }

        public void Write(Exception ex)
        {
            writeExceptionInfo.Invoke(invokeObject, new object[] { ex });
        }

        public void Write(string message, Exception ex)
        {
            writeMessageWithExceptionInfo.Invoke(invokeObject, new object[] { message , ex});
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            // private set { isEnabled = value; }
        }
    }
}

