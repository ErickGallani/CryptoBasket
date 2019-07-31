namespace CryptoBasket.CrossCutting.Extensions.DependencyInjection
{
    using CryptoBasket.CrossCutting.Adapters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdapterFactory(this IServiceCollection service)
        {
            // Add adapter factory
            service.AddSingleton<IAdapterFactory, AdapterFactory>();

            return service;
        }
    }
}