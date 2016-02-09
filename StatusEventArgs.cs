using System;

namespace ConsoleLog
{
    public class StatusEventArgs : EventArgs
    {
        public LogStatus LogStatus { get; private set; }

        public StatusEventArgs(LogStatus status)
        {
            LogStatus = status;
        }
    }
}
