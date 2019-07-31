namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.ErrorCodes;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Domain.Core.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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
        private readonly IErrorLogger errorLogger;

        public PurchaseController(
            IPurchaseService purchaseService, 
            IErrorLogger errorLogger)
        {
            this.purchaseService = purchaseService;
            this.errorLogger = errorLogger;
        }

        [HttpGet(Name = "GetPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Response>> Get(Guid id)
        {
            try
            {
                var purchase = await this.purchaseService.GetPurchaseAsync(id);

                if (purchase is ResponseFailed)
                {
                    var failed = purchase as ResponseFailed;

                    if(failed.Errors.Any(x => x.Code == PurchaseErroCode.PurchaseNotFound))
                    {
                        return NotFound(failed);
                    }

                    return BadRequest(failed);
                }

                return Ok(purchase);
            }
            catch (Exception ex)
            {
                await this.errorLogger.LogAsync(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost(Name = "PostPurchase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Response>> Post(PurchaseDto purchase)
        {
            try
            {
                if (purchase == null)
                {
                    return BadRequestParameter(nameof(purchase));
                }

                var response = await this.purchaseService.PurchaseAsync(purchase);

                if (response is ResponseFailed)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                await this.errorLogger.LogAsync(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}