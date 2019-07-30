namespace CryptoBasket.IoC
{
    using CryptoBasket.Application.Extensions.DependencyInjection;
    using CryptoBasket.CoinMarketCap.Extensions.DependencyInjection;
    using CryptoBasket.ErrorLogger.Extensions.DependencyInjection;
    using CryptoBasket.Repository.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionBootstrap
    {
        public static IServiceCollection Setup(
            IServiceCollection services)
        {
            services
                .AddServices()
                .AddCryptoClients()
                .AddErrorLogger()
                .AddRepositories();

            return services;
        }
    }
}
