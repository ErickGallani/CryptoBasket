namespace CryptoBasket.Application.Tests.Services
{
    using CryptoBasket.Application.Adapters;
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.ErrorCodes;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Application.Services;
    using CryptoBasket.CrossCutting.Adapters;
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Domain.ValueObjects;
    using CryptoBasket.Repository.Interfaces;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
     
    public class PurchaseServiceTests
    {
        private readonly Mock<IPurchaseRepository> purchaseRepositoryMock;
        private readonly Mock<IAdapterFactory> adapterFactoryMock;
        private readonly PurchaseService purchaseService;

        public PurchaseServiceTests()
        {
            purchaseRepositoryMock = new Mock<IPurchaseRepository>();
            adapterFactoryMock = new Mock<IAdapterFactory>();

            purchaseService = new PurchaseService(purchaseRepositoryMock.Object, adapterFactoryMock.Object);
        }

        [Fact]
        public async Task Given_AnNonExistingPurchaseId_When_TryToGetThePurchaseById_Then_NotFoundIsReturned()
        {
            // arrange
            purchaseRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(default(Purchase));

            // act
            var response = await purchaseService.GetPurchaseAsync(Guid.NewGuid());

            // assert
            Assert.NotNull(response);
            Assert.True(response is ResponseFailed);

            var failedResponse = (response as ResponseFailed);

            Assert.True(failedResponse.Errors.Count == 1);
            Assert.Equal(PurchaseErroCode.PurchaseNotFound, failedResponse.Errors.First().Code);
        }

        [Fact]
        public async Task Given_AnExistingPurchaseId_When_TryToGetThePurchaseById_Then_TheRightPurchaseIsReturned()
        {
            // arrange
            var purchase = new Purchase(
                new Product(
                    id: 45,
                    name: "My Own Coin",
                    symbol: "MOC",
                    price: 745D),
                    quantity: 1);

            purchaseRepositoryMock
                .Setup(x => x.GetByIdAsync(It.Is<Guid>(i => i == purchase.Id)))
                .ReturnsAsync(purchase);

            adapterFactoryMock.Setup(a => a.Create<PurchaseToPurchaseDtoAdapter>()).Returns(new PurchaseToPurchaseDtoAdapter());

            // act
            var response = await purchaseService.GetPurchaseAsync(purchase.Id);

            // assert
            Assert.NotNull(response);
            Assert.True(response is ResponseSuccess<PurchaseDto>);

            var successResponse = (response as ResponseSuccess<PurchaseDto>);

            Assert.NotNull(successResponse.Result);
            Assert.Equal(purchase.Quantity, successResponse.Result.Quantity);
            Assert.Equal(purchase.Product.Id, successResponse.Result.Product.Id);
            Assert.Equal(purchase.Product.Name, successResponse.Result.Product.Name);
            Assert.Equal(purchase.Product.Symbol, successResponse.Result.Product.Symbol);
            Assert.Equal(purchase.Product.Price, successResponse.Result.Product.Price);
        }

        [Fact]
        public async Task Given_NullParameterRequest_When_TryToMakeAPurchase_Then_ErrorIsReturned()
        {
            // arrange
            PurchaseDto purchaseDto = null;

            // act
            var response = await purchaseService.PurchaseAsync(purchaseDto);

            // assert
            Assert.NotNull(response);
            Assert.True(response is ResponseFailed);

            var failedResponse = (response as ResponseFailed);

            Assert.True(failedResponse.Errors.Count == 1);
            Assert.Equal(PurchaseErroCode.ParameterPurchaseDtoMandatory, failedResponse.Errors.First().Code);
        }

        [Theory]
        [InlineData(0, "My Own Coin", "MOC", 745D, 1)]
        [InlineData(34, "", "MOC", 745D, 1)]
        [InlineData(34, null, "MOC", 745D, 1)]
        [InlineData(45, "My Own Coin", "", 745D, 1)]
        [InlineData(45, "My Own Coin", null, 745D, 1)]
        [InlineData(654, "My Own Coin", "MOC", 0, 1)]
        [InlineData(87, "My Own Coin", "MOC", 34D, 0)]
        public async Task Given_AnInvalidPurchase_When_TryToMakeAPurchase_Then_FailedResponseIsReturned(
            int productId,
            string productName,
            string productSymbol,
            double productPrice,
            double quantity)
        {
            // arrange
            var purchaseDto = new PurchaseDto(
                new ProductDto(
                    id: productId,
                    name: productName,
                    symbol: productSymbol,
                    price: productPrice),
                    quantity: quantity);

            adapterFactoryMock.Setup(a => a.Create<PurchaseDtoToPurchaseAdapter>()).Returns(new PurchaseDtoToPurchaseAdapter());

            // act
            var response = await purchaseService.PurchaseAsync(purchaseDto);

            // assert
            Assert.NotNull(response);
            Assert.True(response is ResponseFailed);

            var failedResponse = (response as ResponseFailed);

            Assert.True(failedResponse.Errors.Count > 0);
        }

        [Fact]
        public async Task Given_AnValidPurchase_When_TryToMakeAPurchase_Then_SuccessResponseIsReturned()
        {
            // arrange
            var purchaseDto = new PurchaseDto(
                new ProductDto(
                    id: 45,
                    name: "My Own Coin",
                    symbol: "MOC",
                    price: 745D),
                    quantity: 1);

            adapterFactoryMock.Setup(a => a.Create<PurchaseDtoToPurchaseAdapter>()).Returns(new PurchaseDtoToPurchaseAdapter());

            purchaseRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Purchase>())).ReturnsAsync(1);

            // act
            var response = await purchaseService.PurchaseAsync(purchaseDto);

            // assert
            Assert.NotNull(response);
            Assert.True(response is ResponseSuccess<Guid>);

            var successResponse = (response as ResponseSuccess<Guid>);

            Assert.NotEqual(default, successResponse.Result);
        }
    }
}
