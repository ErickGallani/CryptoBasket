namespace CryptoBasket.Application.Adapters
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.CrossCutting.Adapters;
    using CryptoBasket.Domain.Entities;

    public class PurchaseToPurchaseDtoAdapter : IAdapter<Purchase, PurchaseDto>
    {
        public PurchaseDto Adapt(Purchase source)
        {
            PurchaseDto purchaseDto = null;

            if (source != null)
            {
                new PurchaseDto(
                    new ProductDto(
                        source.Product.Id,
                        source.Product.Name,
                        source.Product.Symbol,
                        source.Product.Price),
                    source.Quantity);
            }

            return purchaseDto;
        }
    }
}
