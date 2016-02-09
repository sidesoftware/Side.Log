using System;

namespace ConsoleLog
{
    public class LogStatus
    {
        public LogTypeEnum LogType { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public enum LogTypeEnum
    {
        Default,
        Info,
        Warning,
        Error,
        Subtle,
        Standout,
        Success,
        Debug
    }
}
