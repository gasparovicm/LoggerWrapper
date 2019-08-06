using System;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Empty log methods for Logger class
    /// </summary>
    public class EmptyLogMethods :ILogMethods
    {
        public void Write(string message)
        {
        }

        public void Write(Exception ex)
        {
        }

        public void Write(string message, Exception ex)
        {
        }

        public bool IsEnabled { get { return false; } }


        private static EmptyLogMethods _instance = new EmptyLogMethods();

        public static EmptyLogMethods Instance
        {
            get { return _instance; }
        }
    }
}
