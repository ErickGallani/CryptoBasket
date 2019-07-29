namespace CryptoBasket.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;

    public class ProductService : IProductService
    {
        private readonly ICryptoMarket cryptoMarket;

        public ProductService(ICryptoMarket cryptoMarket)
        {
            this.cryptoMarket = cryptoMarket;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await this.cryptoMarket.GetProductsAsync();

            return products;
        }
    }
}
