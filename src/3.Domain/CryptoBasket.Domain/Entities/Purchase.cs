namespace CryptoBasket.Domain.Entities
{
    using CryptoBasket.Domain.Core.Entities;
    using CryptoBasket.Domain.ValueObjects;
    using System;

    public class Purchase : Entity
    {
        private Purchase() { }

        public Purchase(Product product, int quantity)
        {
            this.Id = Guid.NewGuid();
            this.Product = product;
            this.Quantity = quantity;
        }

        public Product Product { get; private set; }

        public int Quantity { get; private set; }
    }
}
