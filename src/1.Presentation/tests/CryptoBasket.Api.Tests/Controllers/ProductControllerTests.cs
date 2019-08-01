namespace CryptoBasket.Api.Tests.Controllers
{
    using CryptoBasket.Api.Controllers.V1;
    using CryptoBasket.Api.Decorators;
    using CryptoBasket.Api.LinkBuilders.Factory;
    using CryptoBasket.Api.LinkBuilders.Interfaces;
    using CryptoBasket.Api.Models;
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Domain.Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductControllerTests
    {
        private readonly Mock<IErrorLogger> errorLogger;
        private readonly Mock<IProductService> productServiceMock;
        private readonly Mock<ILinkBuilderFactory> linkBuilderFactory;
        private readonly Mock<ILinkBuilder> linkBuilder;
        private readonly ProductController productController;

        public ProductControllerTests()
        {
            this.productServiceMock = new Mock<IProductService>();
            this.errorLogger = new Mock<IErrorLogger>();
            this.linkBuilderFactory = new Mock<ILinkBuilderFactory>();
            this.linkBuilder = new Mock<ILinkBuilder>();

            this.linkBuilder.Setup(l => l.BuildLinks()).Returns(new List<Link>() {
                new Link("https://testing/v1.0/test", "REL", "TEST_TYPE")
            });

            this.linkBuilderFactory.Setup(x => x.Create(It.IsAny<Type>(), It.IsAny<object>())).Returns(this.linkBuilder.Object);

            this.linkBuilderFactory.Setup(x => x.Create(It.IsAny<Type>())).Returns(this.linkBuilder.Object);

            productController = new ProductController(
                this.productServiceMock.Object,
                this.errorLogger.Object,
                this.linkBuilderFactory.Object);
        }

        [Fact]
        public async Task Given_ValidRequest_When_GetProductsAsyncInvoked_Then_200OKIsReturned()
        {
            // arrange
            this.productServiceMock
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new ResponseSuccess<IEnumerable<ProductDto>>(new List<ProductDto>()));

            // act
            var actionResult = await productController.Get();

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is SuccessResponseLinksDecorator);

            var result = (actionResult.Result as OkObjectResult).Value as SuccessResponseLinksDecorator;

            Assert.True(result.Links.Count() > 0);
            Assert.NotNull(result.Response);

            Assert.True(result.Response is ResponseSuccess<IEnumerable<ProductDto>>);
        }

        [Fact]
        public async Task Given_InvalidRequest_When_GetProductsAsyncInvoked_Then_400BadRequestIsReturned()
        {
            // arrange
            this.productServiceMock
                .Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { }));

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

            // act
            var actionResult = await productController.Get();

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }
    }
}
