namespace CryptoBasket.Api.Decorators
{
    using CryptoBasket.Application.Returns;

    public abstract class ResponseDecorator : Response
    {
        public Response Response { get; set; }
    }
}
