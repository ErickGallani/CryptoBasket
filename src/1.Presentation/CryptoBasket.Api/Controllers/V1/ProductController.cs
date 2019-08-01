namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Api.Decorators;
    using CryptoBasket.Api.LinkBuilders;
    using CryptoBasket.Api.LinkBuilders.Factory;
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
        private readonly ILinkBuilderFactory linkBuilderFactory;

        public ProductController(
            IProductService productService, 
            IErrorLogger errorLogger,
            ILinkBuilderFactory linkBuilderFactory)
        {
            this.productService = productService;
            this.errorLogger = errorLogger;
            this.linkBuilderFactory = linkBuilderFactory;
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

                var decorator =
                    new SuccessResponseLinksDecorator(
                        response,
                        this.linkBuilderFactory.Create(typeof(ProductGetLinksBuilder)));

                return Ok(decorator);
            }
            catch (Exception ex)
            {
                await this.errorLogger.LogAsync(ex.Message, ex);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}