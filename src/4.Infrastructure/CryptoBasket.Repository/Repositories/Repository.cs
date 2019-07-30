namespace CryptoBasket.Repository.Repositories
{
    using CryptoBasket.Domain.Core.Entities;
    using CryptoBasket.Repository.Context;
    using CryptoBasket.Repository.Interfaces;
    using System;
    using System.Threading.Tasks;

    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly CryptoBasketContext context;

        public Repository(CryptoBasketContext context) =>
            this.context = context;

        public Task<int> SaveAsync(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChangesAsync();
        }

        public Task<T> GetByIdAsync(Guid id) =>
            context.Set<T>().FindAsync(id);
    }
}
