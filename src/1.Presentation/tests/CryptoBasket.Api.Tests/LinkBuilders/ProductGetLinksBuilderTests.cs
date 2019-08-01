namespace CryptoBasket.Api.Tests.LinkBuilders
{
    using CryptoBasket.Api.LinkBuilders;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Linq;
    using Xunit;

    public class ProductGetLinksBuilderTests
    {
        private readonly Mock<IUrlHelper> urlHelperMock;
        private readonly ProductGetLinksBuilder productGetLinksBuilder;

        public ProductGetLinksBuilderTests()
        {
            urlHelperMock = new Mock<IUrlHelper>();

            urlHelperMock
                .Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns<string, object>((url, val) => $"{url} - {val?.ToString()}");

            productGetLinksBuilder = new ProductGetLinksBuilder(urlHelperMock.Object);
        }

        [Fact]
        public void Given_AValidBuilder_When_BuildLinksIsInvoked_Then_TheLinksAreReturned()
        {
            // act
            var links = productGetLinksBuilder.BuildLinks();

            // assert
            Assert.NotNull(links);
            Assert.True(links.Count() > 0);
        }
    }
}
