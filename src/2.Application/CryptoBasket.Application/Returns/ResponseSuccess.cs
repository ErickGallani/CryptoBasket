namespace CryptoBasket.Application.Returns
{
    using CryptoBasket.Application.Dtos;
    using System.Collections.Generic;

    public class ResponseSuccess : Response
    {
        public ResponseSuccess() =>
            Links = new List<LinkDto>();

        public IEnumerable<LinkDto> Links { get; set; }
    }

    public class ResponseSuccess<TResultValue> : ResponseSuccess
    {
        public ResponseSuccess(TResultValue result) : base() => 
            this.Result = result;

        public TResultValue Result { get; set; }
    }
}
