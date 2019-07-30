namespace CryptoBasket.Domain.Core.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IErrorLogger
    {
        Task LogAsync(string message);

        Task LogAsync(string message, Exception exception);
    }
}
