namespace CryptoBasket.Application.Services
{
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
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
