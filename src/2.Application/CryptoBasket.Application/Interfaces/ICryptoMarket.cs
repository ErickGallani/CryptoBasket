namespace CryptoBasket.Application.Interfaces
{
    using CryptoBasket.Application.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICryptoMarket
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
