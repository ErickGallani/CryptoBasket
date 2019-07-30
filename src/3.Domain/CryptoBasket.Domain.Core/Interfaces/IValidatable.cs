namespace CryptoBasket.Domain.Core.Interfaces
{
    using FluentValidation.Results;
    using System.Threading.Tasks;

    public interface IValidatable
    {
        Task<ValidationResult> ValidateAsync();
    }
}
