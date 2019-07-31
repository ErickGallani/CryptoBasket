namespace CryptoBasket.Application.Tests.Services
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Application.Services;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductServiceTests
    {
        private readonly Mock<ICryptoMarket> cryptoMarketMock;
        private readonly ProductService productService;

        public ProductServiceTests()
        {
            cryptoMarketMock = new Mock<ICryptoMarket>();

            productService = new ProductService(cryptoMarketMock.Object);
        }

        [Fact]
        public async Task Given_FailedRequest_When_RequestProducts_Then_FailedResponseIsReturned()
        {
            // arrange
            cryptoMarketMock.Setup(c => c.GetProductsAsync()).ReturnsAsync(new ResponseFailed(new List<ErrorDto>()));

            // act
            var response = await productService.GetProductsAsync();

            // assert
            Assert.True(response is ResponseFailed);
        }

        [Fact]
        public async Task Given_SuccessRequest_When_RequestProducts_Then_SuccessResponseIsReturned()
        {
            // arrange
            cryptoMarketMock.Setup(c => c.GetProductsAsync()).ReturnsAsync(new ResponseSuccess<IEnumerable<ProductDto>>(new List<ProductDto>()));

            // act
            var response = await productService.GetProductsAsync();

            // assert
            Assert.True(response is ResponseSuccess<IEnumerable<ProductDto>>);
        }
    }
}
