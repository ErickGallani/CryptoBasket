namespace CryptoBasket.Application.Adapters
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.CrossCutting.Adapters;
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Domain.ValueObjects;

    public class PurchaseDtoToPurchaseAdapter : IAdapter<PurchaseDto, Purchase>
    {
        public Purchase Adapt(PurchaseDto source)
        {
            Purchase purchase = null;

            if (source != null)
            {
                purchase = 
                    new Purchase(
                        new Product(
                            source.Product?.Id ?? 0,
                            source.Product?.Name,
                            source.Product?.Symbol,
                            source.Product?.Price ?? 0),
                        source.Quantity);
            }

            return purchase;
        }
    }
}
