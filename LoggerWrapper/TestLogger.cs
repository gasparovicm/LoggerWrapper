using System;
using System.Collections.Generic;

namespace idev.LoggerWrapper
{
    public class TestLogger : LoggerWrapper
    {

        public static List<LogEntry> LogMessages { get; set; }

        public TestLogger(Type type)
        : base(GetLogMethods(type.FullName), type.FullName)
        {
        }

        public TestLogger(string name)
        : base(GetLogMethods(name), name)
        {
        }

        static TestLogger()
        {
            LogMessages = new List<LogEntry>();
        }

        private static LogMethods GetLogMethods(string name)
        {
            return new LogMethods(
                x => LogMessages.Add(new LogEntry(name,x)),
                x => LogMessages.Add(new LogEntry(name,x.Message)) , 
                (x, y) => LogMessages.Add( new LogEntry(name,x + "Error: " + y.Message))
                );
        }

        public class LogEntry
        {
            public readonly string Name;
            public readonly string Message;

            public LogEntry(string name, string message)
            {
                Name = name;
                Message = message;
            }
        }
    }
}
