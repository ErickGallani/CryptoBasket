namespace CryptoBasket.Repository.Interfaces
{
    using CryptoBasket.Domain.Core.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IRepository<T>
       where T : Entity
    {
        Task<T> GetByIdAsync(Guid id);

        Task<int> SaveAsync(T entity);
    }
}
