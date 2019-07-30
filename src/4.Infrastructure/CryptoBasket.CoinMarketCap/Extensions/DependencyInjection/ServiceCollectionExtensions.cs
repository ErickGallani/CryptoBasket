namespace CryptoBasket.CoinMarketCap.Extensions.DependencyInjection
{
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.CoinMarketCap.Client;
    using CryptoBasket.CoinMarketCap.Consts;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCryptoClients(this IServiceCollection services)
        {
            // Add clients
            services.AddHttpClient();

            services.AddScoped<ICryptoMarket, CoinMarketCapClient>();

            services.AddHttpClient(HttpConsts.HTTP_NAME, c =>
            {
                c.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/");
                
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "19b94fb2-7592-4cf7-8f27-de2f9fac435f");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
