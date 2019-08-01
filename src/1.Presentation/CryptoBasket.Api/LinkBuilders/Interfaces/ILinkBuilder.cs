namespace CryptoBasket.Api.LinkBuilders.Interfaces
{
    using CryptoBasket.Api.Models;
    using System.Collections.Generic;

    public interface ILinkBuilder
    {
        IEnumerable<Link> BuildLinks();
    }
}
