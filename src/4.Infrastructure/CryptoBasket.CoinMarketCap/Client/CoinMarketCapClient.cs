namespace CryptoBasket.CoinMarketCap.Client
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.Application.Returns;
    using CryptoBasket.CoinMarketCap.Consts;
    using CryptoBasket.Domain.Core.Interfaces;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CoinMarketCapClient : ICryptoMarket
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IErrorLogger errorLogger;

        public CoinMarketCapClient(
            IHttpClientFactory clientFactory,
            IErrorLogger errorLogger)
        {
            this.clientFactory = clientFactory;
            this.errorLogger = errorLogger;
        }

        public async Task<Response> GetProductsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "v1/cryptocurrency/listings/latest?sort=price");

            var products = new List<ProductDto>();

            try
            {
                var client = this.clientFactory.CreateClient(HttpConsts.HTTP_NAME);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var productsJson = await ParseToJsonObjectFromResultAsync(response);

                    foreach (var item in productsJson.Last.Values())
                    {
                        int.TryParse(item["id"].ToString(), out int productId);

                        double.TryParse(item["quote"]["USD"]["price"].ToString(), out double productPrice);

                        products.Add(new ProductDto()
                        {
                            Id = productId,
                            Name = item["name"].ToString(),
                            Price = productPrice,
                            Symbol = item["symbol"].ToString()

                        });
                    }
                }
                else
                {
                    var failedResponse = await ParseToJsonObjectFromResultAsync(response);

                    var message = failedResponse.GetValue("status")["error_message"].ToString();

                    var code = failedResponse.GetValue("status")["error_code"].ToString();

                    await this.errorLogger.LogAsync(message);

                    return Failed(message, code);
                }
            }
            catch (Exception ex)
            {
                await this.errorLogger.LogAsync(ex.Message, ex);

                return Failed(ex.Message, string.Empty);
            }

            return Success(products);
        }

        private Response Failed(string message, string errorCode)
        {
            var error = new List<ErrorDto>()
            {
                new ErrorDto(message, errorCode)
            };

            return new ResponseFailed(error);
        }

        private Response Success(IEnumerable<ProductDto> products) =>
            new ResponseSuccess<IEnumerable<ProductDto>>(products);

        private async Task<JObject> ParseToJsonObjectFromResultAsync(HttpResponseMessage response)
        {
            var stringResult = await response.Content.ReadAsStringAsync();

            return JObject.Parse(stringResult);
        }
    }
}
