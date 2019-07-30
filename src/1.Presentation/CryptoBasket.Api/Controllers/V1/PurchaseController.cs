namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
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
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService) => 
            this.purchaseService = purchaseService;

        [HttpGet(Name = "GetPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Response>> Get(Guid id)
        {
            return Ok();
        }


        [HttpPost(Name = "PostPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public Task<ActionResult<Response>> Post(PurchaseDto purchase)
        {
            return null;
        }
    }
}