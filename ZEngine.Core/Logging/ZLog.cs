﻿
using System;
using System.Collections.Generic;
using ZEngine.Unity.Core.Logging;

#if USE_NLOG
using NLog;
using NLog.Config;
using NLog.Targets;
#endif

// ReSharper disable CheckNamespace

public static class ZLog
{
#if USE_NLOG
    private static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();
#endif // USE_NLOG

    static ZLog()
    {
#if USE_NLOG
        var config = new LoggingConfiguration();

// Targets where to log to: File and Console
        // var logfile = new FileTarget("logfile")
        // {
        //     FileName = "file.txt"
        // };
        var logconsole = new ConsoleTarget("logconsole");
            
// Rules for mapping loggers to targets            
        config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
        // config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
// Apply config           
        LogManager.Configuration = config;
#endif
    }
    
    private static readonly List<ILogger> LOGGERS = new List<ILogger>();

    public static void Register(ILogger logger)
    {
        LOGGERS.Add(logger);
    }
    
    public static void Info(string message)
    {
#if USE_NLOG
        LOGGER.Info(message);
#endif
        Console.WriteLine(message);

        foreach (var logger in LOGGERS)
        {
           logger.Info(message); 
        }
    }

    public static void Info(object message)
    {
#if USE_NLOG
        LOGGER.Debug(message);
#endif

        Info(message == null ? "null" : message.ToString());
    }
    
    public static void Warn(string message)
    {
        Console.Error.WriteLine(message);
#if USE_NLOG
        LOGGER.Warn(message);
#endif

        foreach (var logger in LOGGERS)
        {
            logger.Warn(message); 
        }
    }
    
    public static void Error(string message)
    {
        Console.Error.WriteLine(message);
#if USE_NLOG
        LOGGER.Error(message);
#endif

        foreach (var logger in LOGGERS)
        {
            logger.Error(message); 
        }
    }
    
    public static void Ex(Exception exception)
    {
        Console.Error.WriteLine(exception);
#if USE_NLOG
        LOGGER.Error(exception);
#endif

        foreach (var logger in LOGGERS)
        {
            logger.Ex(exception); 
        }
    }
}
