namespace CryptoBasket.Api.Tests.LinkBuilders
{
    using CryptoBasket.Api.LinkBuilders;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Linq;
    using Xunit;

    public class PurchasePostLinksBuilderTests
    {
        private readonly Mock<IUrlHelper> urlHelperMock;
        private PurchasePostLinksBuilder purchasePostLinksBuilder;

        public PurchasePostLinksBuilderTests()
        {
            urlHelperMock = new Mock<IUrlHelper>();

            urlHelperMock
                .Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns<string, object>((url, val) => $"{url} - {val?.ToString()}");
        }

        [Fact]
        public void Given_AValidBuilder_When_BuildLinksIsInvoked_Then_TheLinksAreReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            purchasePostLinksBuilder = new PurchasePostLinksBuilder(urlHelperMock.Object, purchaseId);

            // act
            var links = purchasePostLinksBuilder.BuildLinks();

            // assert
            Assert.NotNull(links);
            Assert.True(links.Count() > 0);

            Assert.Contains(purchaseId.ToString(), links.First().Href);
        }
    }
}
