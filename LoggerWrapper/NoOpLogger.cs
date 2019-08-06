using System;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Empty logger implementation of ILogger interface. Will do not log anything.
    /// </summary>
    public class NoOpLogger : LoggerBase
    {
        public static readonly NoOpLogger Instance = new NoOpLogger();
        #region Constructors

        // ReSharper disable once UnusedParameter.Local
        public NoOpLogger(Type type)
        {
        }
        
        // ReSharper disable once UnusedParameter.Local
        public NoOpLogger(string name)
            : this()
        {
        }
        
        // ReSharper disable once UnusedParameter.Local
        public NoOpLogger()
        {
        }
        #endregion Constructors

    }
}
