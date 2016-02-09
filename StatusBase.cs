using System;
using System.Linq;

namespace SideSoftware.Log
{
    public class StatusBase
    {
        public event EventHandler<StatusEventArgs> StatusBroadcast;

        public void LogStatus(string message, LogTypeEnum logType = LogTypeEnum.Info, Exception exception = null)
        {
            OnStatusBroadcast(new LogStatus { Message = message, LogType = logType, Exception = exception });
        }

        public void LogStatus(LogStatus status)
        {
            OnStatusBroadcast(status);
        }

        private void OnStatusBroadcast(LogStatus status)
        {
            if (StatusBroadcast == null) return;

            var eventListeners = StatusBroadcast.GetInvocationList();

            // Raising Event
            for (var index = 0; index < eventListeners.Count(); index++)
            {
                var methodToInvoke = (EventHandler<StatusEventArgs>)eventListeners[index];
                methodToInvoke.BeginInvoke(this, new StatusEventArgs(status), EndAsyncEvent, null);
            }

            // Done Raising Event
        }

        private static void EndAsyncEvent(IAsyncResult iar)
        {
            var ar = (System.Runtime.Remoting.Messaging.AsyncResult)iar;
            var invokedMethod = (EventHandler<StatusEventArgs>)ar.AsyncDelegate;

            try
            {
                invokedMethod.EndInvoke(iar);
            }
            catch
            {
                // Handle any exceptions that were thrown by the invoked method
                // An event listener went kaboom!
            }
        }
    }
}
