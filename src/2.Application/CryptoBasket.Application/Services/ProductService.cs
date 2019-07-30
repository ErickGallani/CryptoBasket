namespace CryptoBasket.Application.Services
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : BaseService, IProductService
    {
        private readonly ICryptoMarket cryptoMarket;

        public ProductService(ICryptoMarket cryptoMarket) => 
            this.cryptoMarket = cryptoMarket;

        public Task<Response> GetProductsAsync() =>
            this.cryptoMarket.GetProductsAsync();
    }
}
