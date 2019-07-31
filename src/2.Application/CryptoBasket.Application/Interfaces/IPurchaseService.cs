namespace CryptoBasket.Application.Interfaces
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Returns;
    using System;
    using System.Threading.Tasks;

    public interface IPurchaseService
    {
        Task<Response> PurchaseAsync(PurchaseDto purchase);

        Task<Response> GetPurchaseAsync(Guid id);
    }
}
