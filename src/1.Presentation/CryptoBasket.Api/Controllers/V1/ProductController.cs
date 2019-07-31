namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.Domain.Core.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IErrorLogger errorLogger;

        public ProductController(
            IProductService productService, 
            IErrorLogger errorLogger)
        {
            this.productService = productService;
            this.errorLogger = errorLogger;
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Response>> Get()
        {
            try
            {
                var response = await this.productService.GetProductsAsync();

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