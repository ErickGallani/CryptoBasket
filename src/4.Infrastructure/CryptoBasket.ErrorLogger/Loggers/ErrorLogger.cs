namespace CryptoBasket.ErrorLogger.Loggers
{
    using System;
    using System.Threading.Tasks;
    using CryptoBasket.Domain.Core.Interfaces;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// This abstraction layer is created so the tooling used to log application information / error
    /// is not attached to the application. This is way is easy to change from a simple file logger
    /// to a NoSQL database for example, an elasticsearch database or anything without impact the application
    /// </summary>
    public class ErrorLogger : IErrorLogger
    {
        private readonly ILogger logger;

        public ErrorLogger(ILoggerFactory loggerFactory) =>
            this.logger = loggerFactory.CreateLogger("Generic logger");

        public Task LogAsync(string message) =>
            Task.Run(() => this.logger?.LogError(message));

        public Task LogAsync(string message, Exception exception) =>
            Task.Run(() => this.logger?.LogError(exception, message));
    }
}
