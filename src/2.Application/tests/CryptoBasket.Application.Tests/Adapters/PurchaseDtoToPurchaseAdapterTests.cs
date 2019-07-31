namespace CryptoBasket.Application.Tests.Adapters
{
    using CryptoBasket.Application.Adapters;
    using CryptoBasket.Application.Dtos;
    using Xunit;

    public class PurchaseDtoToPurchaseAdapterTests
    {
        private readonly PurchaseDtoToPurchaseAdapter purchaseDtoToPurchaseAdapter;

        public PurchaseDtoToPurchaseAdapterTests() =>
             purchaseDtoToPurchaseAdapter = new PurchaseDtoToPurchaseAdapter();

        [Fact]
        public void Given_NullParameter_When_AdapterIsInvoked_Then_NullIsReturned()
        {
            // arrange
            PurchaseDto purchaseDto = null;

            // act
            var result = purchaseDtoToPurchaseAdapter.Adapt(purchaseDto);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Given_AValidPurchaseDto_When_AdapterIsInvoked_Then_AValidPurchaseIsReturned()
        {
            // arrange
            var purchaseDto = new PurchaseDto(
               new ProductDto(
                   id: 45,
                   name: "My Own Coin",
                   symbol: "MOC",
                   price: 745D),
                   quantity: 1);

            // act
            var result = purchaseDtoToPurchaseAdapter.Adapt(purchaseDto);

            // assert
            Assert.NotNull(result);
            Assert.Equal(purchaseDto.Quantity, result.Quantity);
            Assert.Equal(purchaseDto.Product.Id, result.Product.Id);
            Assert.Equal(purchaseDto.Product.Name, result.Product.Name);
            Assert.Equal(purchaseDto.Product.Symbol, result.Product.Symbol);
            Assert.Equal(purchaseDto.Product.Price, result.Product.Price);
        }
    }
}
