namespace CryptoBasket.Application.Services
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Returns;
    using System.Linq;

    public abstract class BaseService
    {
        public Response Success() =>
            new ResponseSuccess();

        public Response Success<TResultValue>(TResultValue result) => 
            new ResponseSuccess<TResultValue>(result);

        protected Response Failed(string message, string code) =>
            Failed(new ErrorDto(message, code));

        protected Response Failed(params ErrorDto[] errors) =>
            new ResponseFailed(errors?.ToList());
    }
}
