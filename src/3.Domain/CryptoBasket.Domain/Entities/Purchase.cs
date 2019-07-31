namespace CryptoBasket.Domain.Entities
{
    using CryptoBasket.Domain.Core.Entities;
    using CryptoBasket.Domain.Validators;
    using CryptoBasket.Domain.ValueObjects;
    using FluentValidation.Results;
    using System;
    using System.Threading.Tasks;

    public class Purchase : Entity
    {
        private Purchase() { }

        public Purchase(Product product, double quantity)
        {
            this.Id = Guid.NewGuid();
            this.Product = product;
            this.Quantity = quantity;
        }

        public Product Product { get; private set; }

        public double Quantity { get; private set; }

        public override Task<ValidationResult> ValidateAsync() =>
            new PurchaseValidator().ValidateAsync(this);
    }
}
