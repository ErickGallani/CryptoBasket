namespace CryptoBasket.Application.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CryptoBasket.Application.Adapters;
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.CrossCutting.Adapters;
    using CryptoBasket.Repository.Interfaces;

    public class PurchaseService : BaseService, IPurchaseService
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IAdapterFactory adapterFactory;

        public PurchaseService(
            IPurchaseRepository purchaseRepository,
            IAdapterFactory adapterFactory)
        {
            this.purchaseRepository = purchaseRepository;
            this.adapterFactory = adapterFactory;
        }

        public async Task<Response> GetPurchase(Guid id)
        {
            var purchase = await this.purchaseRepository.GetByIdAsync(id);

            if (purchase == null)
            {
                return Failed("Purchase not found", "5040");
            }

            var purchaseDto = 
                this.adapterFactory
                    .Create<PurchaseToPurchaseDtoAdapter>()
                    .Adapt(purchase);

            return Success(purchaseDto);
        }

        public async Task<Response> Purchase(PurchaseDto purchaseDto)
        {
            if (purchaseDto == null)
            {
                Failed("Parameter purchaseDto can't be null", "5041");
            }

            var purchase =
                this.adapterFactory
                    .Create<PurchaseDtoToPurchaseAdapter>()
                    .Adapt(purchaseDto);

            var validation = await purchase.ValidateAsync();

            if (validation.IsValid)
            {
                await this.purchaseRepository.SaveAsync(purchase);

                return Success();
            }

            var errors = 
                validation
                    .Errors
                    .Select(e => new ErrorDto(e.ErrorMessage, e.ErrorCode))
                    .ToArray();

            return Failed(errors);
        }
    }
}
