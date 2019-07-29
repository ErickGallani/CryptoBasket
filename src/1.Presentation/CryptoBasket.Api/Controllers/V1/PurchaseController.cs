namespace CryptoBasket.Api.Controllers.V1
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/purchases")]
    public class PurchaseController : ControllerBase
    {
        [HttpGet(Name = "GetPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public Task<ActionResult<object/*PurchaseDto*/>> Get(Guid id)
        {
            return null;
        }


        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public Task<ActionResult<object/*ResponseDto*/>> Post(/*PurchaseDto*/)
        {
            return null;
        }
    }
}