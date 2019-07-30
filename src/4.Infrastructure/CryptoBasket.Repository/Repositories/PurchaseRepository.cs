namespace CryptoBasket.Repository.Repositories
{
    using CryptoBasket.Domain.Entities;
    using CryptoBasket.Repository.Context;
    using CryptoBasket.Repository.Interfaces;

    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CryptoBasketContext context) :
            base(context)
        { }
    }
}
