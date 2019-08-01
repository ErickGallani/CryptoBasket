namespace CryptoBasket.Api.Decorators
{
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using CryptoBasket.Api.Models;
    using CryptoBasket.Application.Returns;
    using System.Collections.Generic;

    public class SuccessResponseLinksDecorator : ResponseDecorator
    {
        public SuccessResponseLinksDecorator(Response response, ILinkBuilder linkBuilder)
        {
            this.Response = response;
            this.Links = linkBuilder.BuildLinks();
        }

        public IEnumerable<Link> Links { get; private set; }
    }
}
