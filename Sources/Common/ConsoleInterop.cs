using System;
using System.Runtime.InteropServices;

namespace Common
{
    // Console interop utility wrapper.
    public static class ConsoleInterop
    {
        // Adds or removes an application-defined HandlerRoutine function from the list of handler functions for the calling process.
        // If no handler function is specified, the function sets an inheritable attribute that determines whether the calling process ignores CTRL+C signals.
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms686016(v=vs.85).aspx
        // http://pinvoke.net/default.aspx/kernel32/SetConsoleCtrlHandler.html
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        // An application-defined function used with the SetConsoleCtrlHandler function.
        // A console process uses this function to handle control signals received by the process.
        // When the signal is received, the system creates a new thread in the process to execute the function.
        private delegate bool EventHandler(CtrlType sig);

        // The type of control signal received by the SetConsoleCtrlHandler handler.
        private enum CtrlType
        {
// ReSharper disable InconsistentNaming
            // CTRL+C.
            CTRL_C_EVENT = 0,

            // CTRL+BREAK.
            CTRL_BREAK_EVENT = 1,

            // The user closes the console either by clicking Close on the console window's window menu, 
            // or by clicking the End Task button command from Task Manager.
            CTRL_CLOSE_EVENT = 2,

            // The user is logging off. This signal is received only by services. 
            // Interactive applications are terminated at logoff, so they are not present when the system sends this signal.
            CTRL_LOGOFF_EVENT = 5,

            // The system is shutting down.
            // Interactive applications are not present by the time the system sends this signal, 
            // therefore it can be received only be services in this situation.
            CTRL_SHUTDOWN_EVENT = 6
// ReSharper restore InconsistentNaming
        }


        // SetConsoleCtrlHandler related.
        private static readonly object setLock;
        private static bool isSet;
        private static readonly EventHandler handler;

        // User-defined handlers.
        private static Action ctrlCHandler;
        private static Action ctrlBreakHandler;
        private static Action closeHandler;
        private static Action logoffHandler;
        private static Action shutdownHandler;


        static ConsoleInterop()
        {
            setLock = new object();
            isSet = false;
            handler += Handler;

            ctrlCHandler = null;
            ctrlBreakHandler = null;
            closeHandler = null;
            logoffHandler = null;
            shutdownHandler = null;
        }


        // SetConsoleCtrlHandler signal type switch and handler selector.
        private static bool Handler(CtrlType sig)
        {
            var result = false;

            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                    if (ctrlCHandler != null)
                    {
                        ctrlCHandler(); 
                        result = true;
                    }
                    break;

                case CtrlType.CTRL_BREAK_EVENT: 
                    if (ctrlBreakHandler != null)
                    {
                        ctrlBreakHandler(); 
                        result = true;
                    }
                    break;

                case CtrlType.CTRL_CLOSE_EVENT: 
                    if (closeHandler != null)
                    {
                        closeHandler(); 
                        result = true;
                    }
                    break;

                case CtrlType.CTRL_LOGOFF_EVENT:
                    if (logoffHandler != null)
                    {
                        logoffHandler(); 
                        result = true;
                    }
                    break;

                case CtrlType.CTRL_SHUTDOWN_EVENT: 
                    if (shutdownHandler != null)
                    {
                        shutdownHandler(); 
                        result = true;
                    }
                    break;
            }

            return result;
        }

        // Sets handlers for console events.
        // If handlers is null then no action will be performed.
        // If all handlers are null then console handler will be removed.
        public static bool SetHandlers(Action ctrlC = null, Action ctrlBreak = null,
            Action close = null, Action logoff = null, Action shutdown = null)
        {
            if (ctrlC == null && ctrlBreak == null && 
                close == null && logoff == null && shutdown == null)
            {
                return ClearHandlers();
            }

            var result = false;

            lock (setLock)
            {
                if (ctrlC != null) 
                    ctrlCHandler = ctrlC;

                if (ctrlBreak != null) 
                    ctrlBreakHandler = ctrlBreak;

                if (close != null) 
                    closeHandler = close;

                if (logoff != null) 
                    logoffHandler = logoff;

                if (shutdown != null) 
                    shutdownHandler = shutdown;

                if (!isSet)
                {
                    result = SetConsoleCtrlHandler(handler, true);
                    isSet = true;
                }
            }

            return result;
        }

        // Sets one handler for all console events.
        public static bool SetHandler(Action any)
        {
            return SetHandlers(any, any, any, any, any);
        }

        // Removes handler from all console events.
        public static bool ClearHandlers()
        {
            var result = false;

            lock (setLock)
            {
                if (isSet)
                {
                    result = SetConsoleCtrlHandler(handler, false);
                    isSet = false;
                }
            }

            return result;
        }
    }
}
