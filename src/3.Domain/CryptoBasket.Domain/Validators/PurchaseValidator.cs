namespace CryptoBasket.Domain.Validators
{
    using CryptoBasket.Domain.Entities;
    using FluentValidation;
    using System;

    public class PurchaseValidator : AbstractValidator<Purchase>
    {
        public PurchaseValidator()
        {
            AddProductValidation();
            AddQuantityValidation();
            AddIdValidation();
        }

        private void AddProductValidation() =>
            RuleFor(purchase => purchase.Product)
                .NotNull()
                    .WithErrorCode("2050")
                    .WithMessage("Product can't be null")
                .SetValidator(purchase => purchase.Product.GetValidator());

        private void AddQuantityValidation() =>
            RuleFor(purchase => purchase.Quantity)
                .GreaterThan(0)
                    .WithErrorCode("2051")
                    .WithMessage("Quantity can't be less than 1");

        private void AddIdValidation() =>
            RuleFor(purchase => purchase.Id)
                .NotEqual(default(Guid))
                    .WithErrorCode("2052")
                    .WithMessage("Invalid purchase Id");
    }
}
