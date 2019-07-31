namespace CryptoBasket.Application.Tests.Adapters
{
    using CryptoBasket.Application.Adapters;
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Domain.ValueObjects;
    using Xunit;

    public class PurchaseToPurchaseDtoAdapterTests
    {
        private readonly PurchaseToPurchaseDtoAdapter purchaseToPurchaseDtoAdapter;

        public PurchaseToPurchaseDtoAdapterTests() =>
            purchaseToPurchaseDtoAdapter = new PurchaseToPurchaseDtoAdapter();

        [Fact]
        public void Given_NullParameter_When_AdapterIsInvoked_Then_NullIsReturned()
        {
            // arrange
            Purchase purchase = null;

            // act
            var result = purchaseToPurchaseDtoAdapter.Adapt(purchase);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Given_AValidPurchaseDto_When_AdapterIsInvoked_Then_AValidPurchaseIsReturned()
        {
            // arrange
            var purchase = new Purchase(
               new Product(
                   id: 45,
                   name: "My Own Coin",
                   symbol: "MOC",
                   price: 745D),
                   quantity: 1);

            // act
            var result = purchaseToPurchaseDtoAdapter.Adapt(purchase);

            // assert
            Assert.NotNull(result);
            Assert.Equal(purchase.Quantity, result.Quantity);
            Assert.Equal(purchase.Product.Id, result.Product.Id);
            Assert.Equal(purchase.Product.Name, result.Product.Name);
            Assert.Equal(purchase.Product.Symbol, result.Product.Symbol);
            Assert.Equal(purchase.Product.Price, result.Product.Price);
        }
    }
}
