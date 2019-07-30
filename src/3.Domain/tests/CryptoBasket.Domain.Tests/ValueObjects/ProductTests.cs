namespace CryptoBasket.Domain.Tests.ValueObjects
{
    using CryptoBasket.Domain.ValueObjects;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductTests
    {
        [Theory]
        [InlineData(0, "Product Name", "PDN", 475D)]
        [InlineData(12, null, "PDN", 475D)]
        [InlineData(12, "", "PDN", 475D)]
        [InlineData(45, "Product Name", null, 475D)]
        [InlineData(45, "Product Name", "", 475D)]
        [InlineData(57, "Product Name", "PDN", 0D)]
        public async Task Given_AInvalidProduct_When_IsValidated_Then_TheValidationFailed(int id, string name, string symbol, double price)
        {
            // arrange
            var product = new Product(id, name, symbol, price);

            // act
            var validator = product.GetValidator();

            var validation = await validator.ValidateAsync(product);

            // assert
            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Count > 0);
        }

        [Fact]
        public async Task Given_AValidProduct_When_IsValidated_Then_TheValidationReturnsSuccess()
        {
            // arrange
            var product = 
                new Product(
                    id: 57, 
                    name: "Product Name", 
                    symbol: "PDN", 
                    price: 856D);

            // act
            var validator = product.GetValidator();

            var validation = await validator.ValidateAsync(product);

            // assert
            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Count > 0);
        }
    }
}
