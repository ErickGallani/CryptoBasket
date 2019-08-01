namespace CryptoBasket.Api.LinkBuilders.Factory
{
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class LinkBuilderFactory : ILinkBuilderFactory
    {
        private readonly IUrlHelper urlHelper;

        public LinkBuilderFactory(IUrlHelper urlHelper) =>
            this.urlHelper = urlHelper;

        public ILinkBuilder Create(Type type, object source)
        {
            switch(type.Name)
            {
                case nameof(PurchasePostLinksBuilder):
                    return new PurchasePostLinksBuilder(this.urlHelper, new Guid(source?.ToString()));
                case nameof(ProductGetLinksBuilder):
                    return new ProductGetLinksBuilder(this.urlHelper);
                case nameof(PurchaseGetLinksBuilder):
                    return new PurchaseGetLinksBuilder(this.urlHelper, new Guid(source?.ToString()));
                default:
                    return null;
            }
        }

        public ILinkBuilder Create(Type type) =>
            Create(type, null);
    }
}
