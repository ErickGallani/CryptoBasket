namespace CryptoBasket.Application.Interfaces
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Returns;
    using System;
    using System.Threading.Tasks;

    public interface IPurchaseService
    {
        Task<Response> Purchase(PurchaseDto purchase);

        Task<Response> GetPurchase(Guid id);
    }
}
