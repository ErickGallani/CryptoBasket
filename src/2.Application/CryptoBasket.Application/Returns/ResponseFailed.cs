namespace CryptoBasket.Application.Returns
{
    using CryptoBasket.Application.Dtos;
    using System.Collections.Generic;

    public class ResponseFailed : Response
    {
        public ResponseFailed(IReadOnlyList<ErrorDto> errors) =>
            this.Errors = errors;

        public IReadOnlyList<ErrorDto> Errors { get; set; }
    }
}
