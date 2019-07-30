namespace CryptoBasket.Domain.Validators
{
    using CryptoBasket.Domain.ValueObjects;
    using FluentValidation;

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            AddIdValidation();
            AddPriceValidation();
            AddSymbolValidation();
            AddNameValidation();
        }

        private void AddIdValidation() =>
            RuleFor(product => product.Id)
                .GreaterThan(0)
                    .WithErrorCode("1050")
                    .WithMessage("Product Id can't be 0");

        private void AddPriceValidation() =>
            RuleFor(product => product.Price)
                .GreaterThan(0)
                    .WithErrorCode("1051")
                    .WithMessage("Price can't be 0");

        private void AddSymbolValidation() =>
            RuleFor(product => product.Symbol)
                .NotNull()
                .NotEmpty()
                    .WithErrorCode("1052")
                    .WithMessage("Symbol can't be null or empty");

        private void AddNameValidation() =>
            RuleFor(product => product.Name)
                .NotNull()
                .NotEmpty()
                    .WithErrorCode("1053")
                    .WithMessage("Name can't be null or empty");
    }
}
