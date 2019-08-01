namespace CryptoBasket.Api.LinkBuilders
{
    using CryptoBasket.Api.Controllers.V1;
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using CryptoBasket.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class ProductGetLinksBuilder : IProductGetLinksBuilder
    {
        private readonly IUrlHelper urlHelper;

        public ProductGetLinksBuilder(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public IEnumerable<Link> BuildLinks()
        {
            var links = new List<Link>
            {
                new Link(this.urlHelper.Link(nameof(PurchaseController.PostPurchase), null),
                "purchases",
                "POST")
            };

            return links;
        }
    }
}
