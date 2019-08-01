namespace CryptoBasket.Api.Extensions.DependencyInjection
{
    using CryptoBasket.Api.LinkBuilders.Factory;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services)
        {
            // Add link builder factory
            services.AddScoped<ILinkBuilderFactory, LinkBuilderFactory>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            return services;
        }
    }
}
