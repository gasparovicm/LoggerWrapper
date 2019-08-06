using System;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Helper interface for initialisation of Logger class
    /// </summary>
    public interface ILogMethods
    {
        void Write(string message);
        void Write(Exception ex);
        void Write(string message, Exception ex);
        bool IsEnabled
        {
            get ; 
        }
    }
}
