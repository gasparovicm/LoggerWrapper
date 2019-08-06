using System.Globalization;

namespace idev.LoggerWrapper
{
    /// <summary>
    /// Logger interface for ClienctCoreContex project.
    /// </summary>
    public interface ILogger :ILoggerBasic
    {
        void FatalFormat(CultureInfo culture , string message, params object[] objects);

        void ErrorFormat(CultureInfo culture, string message, params object[] objects);

        void WarnFormat(CultureInfo culture, string message, params object[] objects);

        void TraceFormat(CultureInfo culture, string message, params object[] objects);

        void DebugFormat(CultureInfo culture, string message, params object[] objects);

        void InfoFormat(CultureInfo culture, string message, params object[] objects);       
    }
}
