namespace CryptoBasket.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : ControllerBase
    {
        protected BadRequestObjectResult BadRequestParameter(string parameterName) =>
            BadRequest(new { Message = $"Parameter {parameterName} can't be null" });
    }
}
