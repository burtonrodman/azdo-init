using System;

namespace azdo_init
{

    public interface ILogger
    {
        void Write(string message, LoggerSeverity severity);
    }

    public enum LoggerSeverity
    {
        Information,
        Action,
        Warning,
        Error
    }

    public static class LoggerExtensions
    {

        public static void WriteInformation(this ILogger logger, string message)
        {
            logger.Write(message, LoggerSeverity.Information);
        }

        public static void WriteAction(this ILogger logger, string message)
        {
            logger.Write(message, LoggerSeverity.Action);
        }

    }

    public class ConsoleLogger : ILogger
    {

        public void Write(string message, LoggerSeverity severity)
        {
            Console.WriteLine($"{severity}, {message}");
        }

    }
}