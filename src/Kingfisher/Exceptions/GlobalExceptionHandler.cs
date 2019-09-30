using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace Kingfisher.Exceptions
{
    public class GlobalExceptionHandler
    {
        public static void Setup(Application app)
        {
            // set up handling of unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            app.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogException(e.ExceptionObject);
        }

        private static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogException(e.Exception);

            e.Handled = true;
        }

        private static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogException(e.Exception);
            e.Handled = true;
        }

        private static void LogException(object exception, [CallerMemberName] string from = null)
        {
            Debug.WriteLine($"UNHANDLED EXCEPTION ({from}): " + exception);
        }
    }
}