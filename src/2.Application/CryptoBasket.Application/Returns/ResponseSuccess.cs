namespace CryptoBasket.Application.Returns
{
    public class ResponseSuccess : Response
    {
        public ResponseSuccess() { }
    }

    public class ResponseSuccess<TResultValue> : ResponseSuccess
    {
        public ResponseSuccess(TResultValue result) : base() => 
            this.Result = result;

        public TResultValue Result { get; set; }
    }
}
