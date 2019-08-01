namespace CryptoBasket.Api.LinkBuilders
{
    using CryptoBasket.Api.Controllers.V1;
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using CryptoBasket.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    public class PurchaseGetLinksBuilder : IPurchaseGetLinksBuilder
    {
        private readonly IUrlHelper urlHelper;

        private Guid id;

        public PurchaseGetLinksBuilder(IUrlHelper urlHelper, Guid id)
        {
            this.id = id;
            this.urlHelper = urlHelper;
        }

        public IEnumerable<Link> BuildLinks()
        {
            var idObj = new { id = this.id };

            var links = new List<Link>
            {
                new Link(this.urlHelper.Link(nameof(PurchaseController.GetPurchase), idObj),
                "purchases",
                "PUT - this url desn't exists, is just an example")
            };

            return links;
        }
    }
}
