namespace CryptoBasket.Api.LinkBuilders.Factory
{
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using System;

    public interface ILinkBuilderFactory
    {
        ILinkBuilder Create(Type type);

        ILinkBuilder Create(Type type, object source);
    }
}
