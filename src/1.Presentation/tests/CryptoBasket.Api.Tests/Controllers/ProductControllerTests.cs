namespace CryptoBasket.Api.Tests.Controllers
{
    using CryptoBasket.Api.Controllers.V1;
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Domain.Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductControllerTests
    {
        private readonly Mock<IErrorLogger> errorLogger;
        private readonly Mock<IProductService> productServiceMock;

        public ProductControllerTests()
        {
            this.productServiceMock = new Mock<IProductService>();
            this.errorLogger = new Mock<IErrorLogger>();
        }

        [Fact]
        public async Task Given_ValidRequest_When_GetProductsAsyncInvoked_Then_200OKIsReturned()
        {
            // arrange
            this.productServiceMock
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new ResponseSuccess<IEnumerable<ProductDto>>(new List<ProductDto>()));

            var productController = new ProductController(this.productServiceMock.Object, this.errorLogger.Object);

            // act
            var actionResult = await productController.Get();

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is ResponseSuccess<IEnumerable<ProductDto>>);
        }

        [Fact]
        public async Task Given_InvalidRequest_When_GetProductsAsyncInvoked_Then_400BadRequestIsReturned()
        {
            // arrange
            this.productServiceMock
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { }));

            var productController = new ProductController(this.productServiceMock.Object, this.errorLogger.Object);

            // act
            var actionResult = await productController.Get();

            // assert
            Assert.True(actionResult.Result is BadRequestObjectResult);
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)HttpStatusCode.BadRequest);
            Assert.True((actionResult.Result as BadRequestObjectResult).Value is ResponseFailed);
        }

        [Fact]
        public async Task Given_ExceptionHappens_When_GetProductsAsyncInvoked_Then_500InternalServerErrorIsReturned()
        {
            // arrange
            this.productServiceMock
                .Setup(x => x.GetProductsAsync())
                .ThrowsAsync(new Exception());

            var productController = new ProductController(this.productServiceMock.Object, this.errorLogger.Object);

            // act
            var actionResult = await productController.Get();

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }
    }
}
