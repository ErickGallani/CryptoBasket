namespace CryptoBasket.IoC
{
    using CryptoBasket.Application.Extensions.DependencyInjection;
    using CryptoBasket.CoinMarketCap.Extensions.DependencyInjection;
    using CryptoBasket.CrossCutting.Extensions.DependencyInjection;
    using CryptoBasket.ErrorLogger.Extensions.DependencyInjection;
    using CryptoBasket.Repository.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionBootstrap
    {
        public static IServiceCollection Setup(
            IServiceCollection services)
        {
            services
                .AddServices()
                .AddCryptoClients()
                .AddErrorLogger()
                .AddRepositories()
                .AddAdapterFactory();

            return services;
        }
    }
}
