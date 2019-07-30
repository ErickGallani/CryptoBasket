namespace CryptoBasket.Application.Dtos
{
    public class ErrorDto
    {
        public ErrorDto(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; set; }

        public string Code { get; set; }
    }
}
