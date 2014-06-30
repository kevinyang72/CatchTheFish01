//#undef USE_NAMED_HIERARCHY

using System;
using System.Globalization;
using System.Reflection;
using log4net.Core;
using log4net.Util;
using log4net.Config;
using System.IO;

namespace Core.Log
{
    /// <summary>
    /// Provides static methods for logging.
    /// </summary>
    /// <remarks>
    /// This is a light wrapper around log4net. It provides set of static logging methods that can
    /// be called anywhere without prior initialization. The log output targets and formatting can
    /// be configured in the standard log4net fashion.
    /// </remarks>
    public static class StaticLog
    {
#if !USE_NAMED_HIERARCHY
        /// <summary>
        /// The singleton logger instance used for all logging output.
        /// </summary>
        private static readonly ILogger Logger = InitializeLogger();
#endif

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Trace"/> level.
        /// </summary>
        /// <param name="format">The message format string to log.</param>
        /// <param name="args">An array containing the message format parameters.</param>
        public static void Trace(string format, params object[] args)
        {
            LogMessage(Level.Trace, format, args);
        }

        /// <summary>
        /// Logs a message string with the <see cref="Level.Trace"/> level.
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Trace(string message)
        {
            LogMessage(Level.Trace, message);
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="format">The message format string to log.</param>
        /// <param name="args">An array containing the message format parameters.</param>
        public static void Debug(string format, params object[] args)
        {
            LogMessage(Level.Debug, format, args);
        }

        /// <summary>
        /// Logs a message string with the <see cref="Level.Debug"/> level.
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Debug(string message)
        {
            LogMessage(Level.Debug, message);
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="format">The message format string to log.</param>
        /// <param name="args">An array containing the message format parameters.</param>
        public static void Info(string format, params object[] args)
        {
            LogMessage(Level.Info, format, args);
        }

        /// <summary>
        /// Logs a message string with the <see cref="Level.Info"/> level.
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Info(string message)
        {
            LogMessage(Level.Info, message);
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="format">The message format string to log.</param>
        /// <param name="args">An array containing the message format parameters.</param>
        public static void Warn(string format, params object[] args)
        {
            LogMessage(Level.Warn, format, args);
        }

        /// <summary>
        /// Logs a message string with the <see cref="Level.Warn"/> level.
        /// </summary>
        /// <param name="message">The message string to log.</param>
        public static void Warn(string message)
        {
            LogMessage(Level.Warn, message);
        }

        /// <summary>
        /// Logs an exception with the <see cref="Level.Error"/> level.
        /// </summary>
        /// <param name="ex">The exception to log, including its stack trace.</param>
        public static void Error(Exception ex)
        {
            LogException(Level.Error, "Error.", ex);
        }

        public static void Error(string message, Exception ex)
        {
            LogException(Level.Error, message, ex);
        }

        public static void Error(string message)
        {
            LogMessage(Level.Error, message);
        }
        /// <summary>
        /// Logs an exception with the <see cref="Level.Fatal"/> level.
        /// </summary>
        /// <param name="ex">The exception to log, including its stack trace.</param>
        public static void Fatal(Exception ex)
        {
            LogException(Level.Fatal, "Fatal error.", ex);
        }

        public static void Fatal(string message, Exception ex)
        {
            LogException(Level.Fatal, message, ex);
        }

        /// <summary>
        /// Logs a formatted message string with the specified <see cref="Level"/>.
        /// </summary>
        private static void LogMessage(Level level, string format, params object[] args)
        {
            var logger = GetLoggerInstance();
            if (logger.IsEnabledFor(level))
            {
                var message = new SystemStringFormat(CultureInfo.InvariantCulture, format, args);
                logger.Log(typeof(Log), level, message, null);
            }
        }

        /// <summary>
        /// Logs a message string with the specified <see cref="Level"/>.
        /// </summary>
        private static void LogMessage(Level level, string message)
        {
            var logger = GetLoggerInstance();
            if (logger.IsEnabledFor(level))
            {
                logger.Log(typeof(Log), level, message, null);
            }
        }

        /// <summary>
        /// Logs an exception with the specified <see cref="Level"/>.
        /// </summary>
        private static void LogException(Level level, string message, Exception ex)
        {
            var logger = GetLoggerInstance();
            if (logger.IsEnabledFor(level))
            {
                logger.Log(typeof(Log), level, message, ex);
            }
        }

#if !USE_NAMED_HIERARCHY
        /// <summary>
        /// Initializes the singleton logger instance.
        /// </summary>
        private static ILogger InitializeLogger()
        {
            const string name = "*";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(
                                                              AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                              AppDomain.CurrentDomain.SetupInformation.ApplicationName + ".config")));
            return LoggerManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }

        /// <summary>
        /// Retrieves the singleton logger instance.
        /// </summary>
        private static ILogger GetLoggerInstance()
        {
            return Logger;
        }
#else
        /// <summary>
        /// Retrieves a logger based on the current state of the stack frame. 
        /// </summary>
        private static ILogger GetLoggerInstance()
        {
            var stackFrame = new System.Diagnostics.StackFrame(3);
            var method = stackFrame.GetMethod();
            var type = method.ReflectedType;
            return LoggerManager.GetLogger(Assembly.GetCallingAssembly(), type);
        }
#endif
    }
}
