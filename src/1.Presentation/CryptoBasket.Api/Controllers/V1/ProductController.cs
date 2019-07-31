namespace CryptoBasket.Api.Controllers.V1
{
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService) => 
            this.productService = productService;

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Response>> Get()
        {
            var response = await this.productService.GetProductsAsync();

            if (response is ResponseFailed)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}