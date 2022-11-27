using Microsoft.Extensions.Logging;
using System;

namespace Educative.Infrastructure.LoggerFactory
{
    public class FactoryLogger<T> where T : class
    {
        public static void InitialiseLoggerFactory(ILoggerFactory loggerFactory, Exception ex)
        {
            var logger = loggerFactory.CreateLogger<T>();
            logger.LogError("Error found!", ex.Message);
        }
    }
}