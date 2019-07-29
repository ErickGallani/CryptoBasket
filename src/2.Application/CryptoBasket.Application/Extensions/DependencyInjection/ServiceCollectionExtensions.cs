﻿namespace CryptoBasket.CoinMarketCap.Extensions.DependencyInjection
{
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // add services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            return services;
        }
    }
}
