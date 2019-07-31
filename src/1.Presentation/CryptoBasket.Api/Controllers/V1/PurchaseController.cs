namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/purchases")]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService) => 
            this.purchaseService = purchaseService;

        [HttpGet(Name = "GetPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Response>> Get(Guid id)
        {
            var purchase = await this.purchaseService.GetPurchase(id);

            if (purchase is ResponseFailed)
            {
                var failed = purchase as ResponseFailed;

                if(failed.Errors.Any(x => x.Code == "5040"))
                {
                    return NotFound(failed);
                }

                return BadRequest(failed);
            }

            return Ok(purchase);
        }

        [HttpPost(Name = "PostPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Response>> Post(PurchaseDto purchase)
        {
            if(purchase == null)
            {
                return BadRequestParameter(nameof(purchase));
            }

            var response = await this.purchaseService.Purchase(purchase);

            if (response is ResponseFailed)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}