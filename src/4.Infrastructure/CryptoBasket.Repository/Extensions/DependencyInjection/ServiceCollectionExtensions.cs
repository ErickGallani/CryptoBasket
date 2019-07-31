namespace CryptoBasket.Repository.Extensions.DependencyInjection
{
    using CryptoBasket.Repository.Context;
    using CryptoBasket.Repository.Interfaces;
    using CryptoBasket.Repository.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add repository
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();

            // Adding Db Context
            services
                .AddDbContext<CryptoBasketContext>(
                    options => options.UseInMemoryDatabase("InMemoryPaymentGatewayDataBase"),
                    ServiceLifetime.Scoped);

            return services;
        }
    }
}
