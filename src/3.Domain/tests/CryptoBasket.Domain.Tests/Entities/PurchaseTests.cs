namespace CryptoBasket.Domain.Tests.Entities
{
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Domain.ValueObjects;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class PurchaseTests
    {
        [Theory]
        [MemberData(nameof(GetPurchaseInvalidDataSet))]
        public async Task Given_AInvalidPurchase_When_ValidationIsDone_Then_FailedIsReturned(Product product, int quantity)
        {
            // arrange
            var purchase = new Purchase(product, quantity);

            // act
            var validation = await purchase.ValidateAsync();

            // assert
            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public async Task Given_AValidPurchase_When_ValidationIsDone_Then_SuccessIsReturned()
        {
            // arrange
            var purchase = 
                new Purchase(
                    new Product(id: 34, name: "Bitcoin", "BTC", 765.6D), 
                    1);

            // act
            var validation = await purchase.ValidateAsync();

            // assert
            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Count > 0);
        }

        public static IEnumerable<object[]> GetPurchaseInvalidDataSet()
        {
            yield return new object[] { null, 3 };
            yield return new object[] { new Product(5, "test", "TST", 78D), 0 };
            yield return new object[] { new Product(0, "Product Name", "PDN", 475D), 1 };
            yield return new object[] { new Product(12, null, "PDN", 475D), 1 };
            yield return new object[] { new Product(12, "", "PDN", 475D), 1 };
            yield return new object[] { new Product(45, "Product Name", null, 475D), 1 };
            yield return new object[] { new Product(45, "Product Name", "", 475D), 1 };
            yield return new object[] { new Product(57, "Product Name", "PDN", 0D), 1 };
        }
    }
}
