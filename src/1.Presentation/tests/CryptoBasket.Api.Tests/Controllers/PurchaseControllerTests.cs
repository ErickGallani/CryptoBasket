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

    public class PurchaseControllerTests
    {
        private readonly Mock<IErrorLogger> errorLogger;
        private readonly Mock<IPurchaseService> purchaseService;

        public PurchaseControllerTests()
        {
            this.errorLogger = new Mock<IErrorLogger>();
            this.purchaseService = new Mock<IPurchaseService>();
        }

        [Fact]
        public async Task Given_Get_NonExistingPurchase_When_TryToGetThePurchaseById_Then_NotFoundIsReturned()
        {
            // arrange
            var purchaseId = Guid.NewGuid();

            this.purchaseService
                .Setup(x => x.GetPurchaseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new ResponseFailed(new List<ErrorDto>() { new ErrorDto("Purchase not found", "5040") }));

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Get(purchaseId);

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

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Get(purchaseId);

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

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Get(purchaseId);

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Given_Get_AValidPurchaseId_When_TryToGetThePurchaseById_Then_BadRequestIsReturned()
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

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Get(purchaseId);

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is ResponseSuccess<PurchaseDto>);

            var result = (actionResult.Result as OkObjectResult).Value as ResponseSuccess<PurchaseDto>;

            Assert.Equal(purchase.Quantity, result.Result.Quantity);
            Assert.Equal(purchase.Product, result.Result.Product);
        }

        [Fact]
        public async Task Given_Post_InvalidParameter_When_TryToMakeAPurchase_Then_BadRequestIsReturned()
        {
            // arrange
            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Post(null);

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

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Post(purchase);

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

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Post(purchase);

            // assert
            Assert.True(actionResult.Result is ObjectResult);
            Assert.Equal((actionResult.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Given_Post_ValidPurchase_When_TryToMakeAPurchase_Then_OkIsReturned()
        {
            // arrange
            var purchase = new PurchaseDto();

            this.purchaseService
                .Setup(x => x.PurchaseAsync(It.IsAny<PurchaseDto>()))
                .ReturnsAsync(new ResponseSuccess());

            var purchaseController = new PurchaseController(this.purchaseService.Object, this.errorLogger.Object);

            // act
            var actionResult = await purchaseController.Post(purchase);

            // assert
            Assert.True(actionResult.Result is OkObjectResult);
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.True((actionResult.Result as OkObjectResult).Value is ResponseSuccess);
        }
    }
}
