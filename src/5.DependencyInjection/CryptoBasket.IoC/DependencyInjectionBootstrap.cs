namespace CryptoBasket.IoC
{
    using CryptoBasket.CoinMarketCap.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionBootstrap
    {
        public static IServiceCollection Setup(
            IServiceCollection services)
        {
            services
                .AddServices()
                .AddCryptoClients();

            return services;
        }
    }
}
