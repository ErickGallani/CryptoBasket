namespace CryptoBasket.Domain.Core.Entities
{
    using CryptoBasket.Domain.Core.Interfaces;
    using FluentValidation.Results;
    using System;
    using System.Threading.Tasks;

    public abstract class Entity : IValidatable
    {
        public Guid Id { get; set; }

        public abstract Task<ValidationResult> ValidateAsync();
    }
}
