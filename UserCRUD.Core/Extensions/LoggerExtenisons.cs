using Microsoft.Extensions.Logging;
using System;

namespace UserCRUD.Core.Extensions
{
    public static class LoggerExtenisons
    {
        public static void CustomLogInformation(this ILogger logger, string message)
        {
            logger.LogInformation(new EventId((int)LogLevel.Information), message.ToLogMessage());
        }

        public static void CustomLogWarning(this ILogger logger, string message)
        {
            logger.LogWarning(new EventId((int)LogLevel.Warning), message.ToLogMessage());
        }

        public static void CustomLogError(this ILogger logger, string message)
        {
            logger.LogError(new EventId((int)LogLevel.Error), message.ToLogMessage());
        }

        public static void CustomLogError(this ILogger logger, Exception ex, string message)
        {
            logger.LogError(new EventId((int)LogLevel.Error), ex, message.ToLogMessage());
        }

        public static void CustomLogCritical(this ILogger logger, string message)
        {
            logger.LogCritical(new EventId((int)LogLevel.Critical), message.ToLogMessage());
        }

        public static void CustomLogCritical(this ILogger logger, Exception ex, string message)
        {
            logger.LogCritical(new EventId((int)LogLevel.Critical), ex, message.ToLogMessage());
        }
    }
}
