namespace CryptoBasket.ErrorLogger.Extensions.DependencyInjection
{
    using CryptoBasket.Domain.Core.Interfaces;
    using CryptoBasket.ErrorLogger.Loggers;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddErrorLogger(this IServiceCollection services)
        {
            // Error notification logger
            services.AddScoped<IErrorLogger, ErrorLogger>();

            return services;
        }
    }
}
