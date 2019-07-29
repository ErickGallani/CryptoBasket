namespace CryptoBasket.CoinMarketCap.Client
{
    using CryptoBasket.Application.Dtos;
    using CryptoBasket.Application.Interfaces;
    using CryptoBasket.CoinMarketCap.Consts;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class CoinMarketCapClient : ICryptoMarket
    {
        private readonly IHttpClientFactory clientFactory;

        public CoinMarketCapClient(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "v1/cryptocurrency/listings/latest?sort=price&limit=0");

            var products = new List<ProductDto>();

            try
            {
                var client = this.clientFactory.CreateClient(HttpConsts.HTTP_NAME);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var stringResult = await response.Content.ReadAsStringAsync();

                    var productsJson = JObject.Parse(stringResult);

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
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return products;
        }
    }
}
