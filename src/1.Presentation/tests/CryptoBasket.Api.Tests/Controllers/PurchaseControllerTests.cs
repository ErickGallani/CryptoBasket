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

    public class PurchaseControllerTests
    {
        private readonly Mock<IErrorLogger> errorLogger;
        private readonly Mock<IPurchaseService> purchaseService;
        private readonly Mock<ILinkBuilderFactory> linkBuilderFactory;
        private readonly Mock<ILinkBuilder> linkBuilder;
        private readonly PurchaseController purchaseController;

        public PurchaseControllerTests()
        {
            this.errorLogger = new Mock<IErrorLogger>();
            this.purchaseService = new Mock<IPurchaseService>();
            this.linkBuilderFactory = new Mock<ILinkBuilderFactory>();
            this.linkBuilder = new Mock<ILinkBuilder>();

            this.linkBuilder.Setup(l => l.BuildLinks()).Returns(new List<Link>() {
                new Link("https://testing/v1.0/test", "REL", "TEST_TYPE")
            });

            this.linkBuilderFactory.Setup(x => x.Create(It.IsAny<Type>(), It.IsAny<object>())).Returns(this.linkBuilder.Object);

            this.linkBuilderFactory.Setup(x => x.Create(It.IsAny<Type>())).Returns(this.linkBuilder.Object);

            purchaseController = new PurchaseController(
                this.purchaseService.Object,
                this.errorLogger.Object,
                this.linkBuilderFactory.Object);
        }

        [Fact]
        public async Task Given_Get_NonExistingPurchase_When_TryToGetThePurchaseById_Then_NotFoundIsReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            this.purchaseService
                .Setup(x => x.GetPurchaseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { new ErrorDto("Purchase not found", "5040") }));

            // act
            var actionResult = await purchaseController.GetPurchase(purchaseId);

            // assert
            Assert.True(actionResult.Result is NotFoundObjectResult);
            Assert.Equal((actionResult.Result as NotFoundObjectResult).StatusCode, (int)HttpStatusCode.NotFound);
            Assert.True((actionResult.Result as NotFoundObjectResult).Value is ResponseFailed);
        }

        [Fact]
        public async Task Given_Get_AnErrorHappen_When_TryToGetThePurchaseById_Then_BadRequestIsReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            this.purchaseService
                .Setup(x => x.GetPurchaseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { new ErrorDto("Unexpected error", "7844") }));

            // act
            var actionResult = await purchaseController.GetPurchase(purchaseId);

            // assert
            Assert.True(actionResult.Result is BadRequestObjectResult);
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)HttpStatusCode.BadRequest);
            Assert.True((actionResult.Result as BadRequestObjectResult).Value is ResponseFailed);
        }

        [Fact]
        public async Task Given_Get_AnExceptionHappen_When_TryToGetThePurchaseById_Then_InternalServerErrorIsReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            this.purchaseService
                .Setup(x => x.GetPurchaseAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            // act
            var actionResult = await purchaseController.GetPurchase(purchaseId);

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Given_Get_AValidPurchaseId_When_TryToGetThePurchaseById_Then_ThePurchaseIsReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            var purchase = 
                new PurchaseDto(
                    new ProductDto(
                        id: 57,
                        name: "My Own Coin",
                        symbol: "MOC",
                        price: 784), 
                    quantity: 2);

            this.purchaseService
                .Setup(x => x.GetPurchaseAsync(It.Is<Guid>(g => g == purchaseId)))
                .ReturnsAsync(new ResponseSuccess<PurchaseDto>(purchase));

            // act
            var actionResult = await purchaseController.GetPurchase(purchaseId);

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is SuccessResponseLinksDecorator);

            var result = (actionResult.Result as OkObjectResult).Value as SuccessResponseLinksDecorator;

            Assert.True(result.Links.Count() > 0);
            Assert.NotNull(result.Response);

            Assert.True(result.Response is ResponseSuccess<PurchaseDto>);

            var response = result.Response as ResponseSuccess<PurchaseDto>;

            Assert.Equal(purchase.Quantity, response.Result.Quantity);
            Assert.Equal(purchase.Product, response.Result.Product);
        }

        [Fact]
        public async Task Given_Post_InvalidParameter_When_TryToMakeAPurchase_Then_BadRequestIsReturned()
        {
            // arrange
            

            // act
            var actionResult = await purchaseController.PostPurchase(null);

            // assert
            Assert.True(actionResult.Result is BadRequestObjectResult);
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Given_Post_AnErrorHappen_When_TryToMakeAPurchase_Then_BadRequestIsReturned()
        {
            // arrange
            var purchase = new PurchaseDto();

            this.purchaseService
                .Setup(x => x.PurchaseAsync(It.IsAny<PurchaseDto>()))
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { new ErrorDto("Unexpected error", "7844") }));

            // act
            var actionResult = await purchaseController.PostPurchase(purchase);

            // assert
            Assert.True(actionResult.Result is BadRequestObjectResult);
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)HttpStatusCode.BadRequest);
            Assert.True((actionResult.Result as BadRequestObjectResult).Value is ResponseFailed);
        }

        [Fact]
        public async Task Given_Post_AnExceptionHappen_When_TryToMakeAPurchase_Then_InternalServerErrorIsReturned()
        {
            // arrange
            var purchase = new PurchaseDto();

            this.purchaseService
                .Setup(x => x.PurchaseAsync(It.IsAny<PurchaseDto>()))
                .ThrowsAsync(new Exception());

            // act
            var actionResult = await purchaseController.PostPurchase(purchase);

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Given_Post_ValidPurchase_When_TryToMakeAPurchase_Then_OkIsReturned()
        {
            // arrange
            var createdPurchaseId = Guid.NewGuid();

            var purchase = new PurchaseDto();

            this.purchaseService
                .Setup(x => x.PurchaseAsync(It.IsAny<PurchaseDto>()))
                .ReturnsAsync(new ResponseSuccess<Guid>(createdPurchaseId));

            // act
            var actionResult = await purchaseController.PostPurchase(purchase);

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is SuccessResponseLinksDecorator);

            var result = (actionResult.Result as OkObjectResult).Value as SuccessResponseLinksDecorator;

            Assert.True(result.Links.Count() > 0);
            Assert.NotNull(result.Response);

            Assert.True(result.Response is ResponseSuccess<Guid>);

            var response = result.Response as ResponseSuccess<Guid>;

            Assert.Equal(createdPurchaseId, response.Result);
        }
    }
}
