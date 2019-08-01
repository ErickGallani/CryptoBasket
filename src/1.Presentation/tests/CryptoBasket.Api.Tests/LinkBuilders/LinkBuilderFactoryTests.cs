namespace CryptoBasket.Api.Tests.LinkBuilders
{
    using CryptoBasket.Api.LinkBuilders;
    using CryptoBasket.Api.LinkBuilders.Factory;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using Xunit;

    public class LinkBuilderFactoryTests
    {
        private readonly Mock<IUrlHelper> urlHelperMock;
        private readonly LinkBuilderFactory linkBuilderFactory;

        public LinkBuilderFactoryTests()
        {
            urlHelperMock = new Mock<IUrlHelper>();

            linkBuilderFactory = new LinkBuilderFactory(urlHelperMock.Object);
        }

        [Fact]
        public void Given_ProductGetLinksBuilderIsRequested_When_CreateIsInvoked_Then_BuilderIsReturnedWithSuccess()
        {
            // act
            var productGetLinksBuilder = linkBuilderFactory.Create(typeof(ProductGetLinksBuilder));

            // assert
            Assert.NotNull(productGetLinksBuilder);
        }

        [Fact]
        public void Given_PurchaseGetLinksBuilderIsRequested_When_CreateIsInvoked_Then_BuilderIsReturnedWithSuccess()
        {
            // act
            var purchaseGetLinksBuilder = linkBuilderFactory.Create(typeof(PurchaseGetLinksBuilder), Guid.NewGuid());

            // assert
            Assert.NotNull(purchaseGetLinksBuilder);
        }

        [Fact]
        public void Given_PurchasePostLinksBuilderIsRequested_When_CreateIsInvoked_Then_BuilderIsReturnedWithSuccess()
        {
            // act
            var purchasePostLinksBuilder = linkBuilderFactory.Create(typeof(PurchasePostLinksBuilder), Guid.NewGuid());

            // assert
            Assert.NotNull(purchasePostLinksBuilder);
        }

        [Fact]
        public void Given_UnkownBuilderTypeIsRequested_When_CreateIsInvoked_Then_NullIsReturned()
        {
            // act
            var purchasePostLinksBuilder = linkBuilderFactory.Create(typeof(string));

            // assert
            Assert.Null(purchasePostLinksBuilder);
        }
    }
}
