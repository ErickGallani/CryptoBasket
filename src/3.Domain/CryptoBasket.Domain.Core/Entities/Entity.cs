namespace CryptoBasket.Domain.Core.Entities
{
    using System;

    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
