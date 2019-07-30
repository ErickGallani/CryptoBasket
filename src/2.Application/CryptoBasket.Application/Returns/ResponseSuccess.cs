namespace CryptoBasket.Application.Returns
{
    using CryptoBasket.Application.Dtos;
    using System.Collections.Generic;

    public class ResponseSuccess<TResultValue> : Response
    {
        public ResponseSuccess() =>
            Links = new List<LinkDto>();

        public ResponseSuccess(TResultValue result) : this() => 
            this.Result = result;

        public TResultValue Result { get; set; }

        public IEnumerable<LinkDto> Links { get; set; }
    }
}
