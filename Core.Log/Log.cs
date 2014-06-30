using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using log4net;
using log4net.Config;

namespace Core.Log
{
    /// <summary>
    /// Log4Net wrapper, lifted from codecampserver (http://code.google.com/p/codecampserver/source/list)
    /// </summary>
    public static class Log
    {
        private static readonly Dictionary<Type, ILog> Loggers = new Dictionary<Type, ILog>();
        private static bool _logInitialized;
        private static readonly object Lock = new object();

        public static string SerializeException(Exception exception)
        {
            return SerializeException(exception, string.Empty);
        }

        private static string SerializeException(Exception e, string exceptionMessage)
        {
            if (e == null) return string.Empty;

            exceptionMessage = string.Format(CultureInfo.InvariantCulture,
                                             "{0}{1}{2}\n{3}",
                                             exceptionMessage,

                                             string.IsNullOrEmpty(exceptionMessage) ? string.Empty : "\n\n",
                                             e.Message,
                                             e.StackTrace);

            if (e.InnerException != null)
                exceptionMessage = SerializeException(e.InnerException, exceptionMessage);

            return exceptionMessage;
        }

        private static ILog GetLogger(Type source)
        {
            EnsureInitialized();
            lock (Lock)
            {
                if (Loggers.ContainsKey(source))
                {
                    return Loggers[source];
                }
                ILog logger = LogManager.GetLogger(source);
                Loggers.Add(source, logger);
                return logger;
            }
        }

        /* Log a message object */

        public static void Debug(object source, string message)
        {
            Debug(source.GetType(), message);
        }

        public static void Debug(object source, string message, params object[] ps)
        {
            Debug(source.GetType(), string.Format(message, ps));
        }

        public static void Debug(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }

        public static void Info(object source, object message)
        {
            Info(source.GetType(), message);
        }

        public static void Info(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }

        public static void Warn(object source, object message)
        {
            Warn(source.GetType(), message);
        }

        public static void Warn(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsWarnEnabled)
                logger.Warn(message);
        }

        public static void Error(object source, object message)
        {
            Error(source.GetType(), message);
        }

        public static void Error(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsErrorEnabled)
                logger.Error(message);
        }

        public static void Fatal(object source, object message)
        {
            Fatal(source.GetType(), message);
        }

        public static void Fatal(Type source, object message)
        {
            ILog logger = GetLogger(source);
            if (logger.IsFatalEnabled)
                logger.Fatal(message);
        }

        /* Log a message object and exception */

        public static void Debug(object source, object message, Exception exception)
        {
            Debug(source.GetType(), message, exception);
        }

        public static void Debug(Type source, object message, Exception exception)
        {
            GetLogger(source).Debug(message, exception);
        }

        public static void Info(object source, object message, Exception exception)
        {
            Info(source.GetType(), message, exception);
        }

        public static void Info(Type source, object message, Exception exception)
        {
            GetLogger(source).Info(message, exception);
        }

        public static void Warn(object source, object message, Exception exception)
        {
            Warn(source.GetType(), message, exception);
        }

        public static void Warn(Type source, object message, Exception exception)
        {
            GetLogger(source).Warn(message, exception);
        }

        public static void Error(object source, object message, Exception exception)
        {
            Error(source.GetType(), message, exception);
        }

        public static void Error(Type source, object message, Exception exception)
        {
            GetLogger(source).Error(message, exception);
        }

        public static void Fatal(object source, object message, Exception exception)
        {
            Fatal(source.GetType(), message, exception);
        }

        public static void Fatal(Type source, object message, Exception exception)
        {
            GetLogger(source).Fatal(message, exception);
        }

        private static void Initialize()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(
                                                               AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                               AppDomain.CurrentDomain.SetupInformation.ApplicationName + ".config")));
            _logInitialized = true;

        }

        public static void EnsureInitialized()
        {
            if (!_logInitialized)
            {
                Initialize();

            }
        }
    }
}