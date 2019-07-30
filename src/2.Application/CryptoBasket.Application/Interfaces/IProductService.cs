namespace CryptoBasket.Application.Interfaces
{
    using CryptoBasket.Application.Returns;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<Response> GetProductsAsync();
    }
}
