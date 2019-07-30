namespace CryptoBasket.Application.Interfaces
{
    using CryptoBasket.Application.Returns;
    using System.Threading.Tasks;

    public interface ICryptoMarket
    {
        Task<Response> GetProductsAsync();
    }
}
