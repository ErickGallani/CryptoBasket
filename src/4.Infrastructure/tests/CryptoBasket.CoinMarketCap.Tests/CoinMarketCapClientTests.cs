namespace CryptoBasket.CoinMarketCap.Tests
{
    using CryptoBasket.CoinMarketCap.Client;
    using Moq;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class CoinMarketCapClientTests
    {
        private readonly Mock<IHttpClientFactory> httpClientFactoryMock;
        private readonly CoinMarketCapClient coinMarketCapClient;

        public CoinMarketCapClientTests()
        {
            this.httpClientFactoryMock = new Mock<IHttpClientFactory>();
            this.coinMarketCapClient = new CoinMarketCapClient(this.httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task Given_WhenValidRequest_When_GetProductsIsInvoked_Then_TheValidListOfProductsIsReturned()
        {
            // arrange
            this.httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(new SuccessHttpMessageHandlerMock()) { BaseAddress = new Uri("https://pro-api.coinmarketcap.com/") });

            // act
            var products = await this.coinMarketCapClient.GetProductsAsync();

            // assert
            Assert.NotNull(products);
            Assert.True(products.Count() > 0);
        }

        [Fact]
        public async Task Given_WhenInvalidQueryParameter_When_GetProductsIsInvoked_Then_AnEmptyProdutctListIsReturned()
        {
            // arrange
            this.httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(new BadRequestHttpMessageHandlerMock()) { BaseAddress = new Uri("https://pro-api.coinmarketcap.com/") });

            // act
            var products = await this.coinMarketCapClient.GetProductsAsync();

            // assert
            Assert.NotNull(products);
            Assert.True(products.Count() == 0);
        }

        [Fact]
        public async Task Given_BadFormattedUrl_When_GetProductsIsInvoked_Then_AnEmptyProdutctListIsReturned()
        {
            // arrange
            this.httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(new SuccessHttpMessageHandlerMock()) { BaseAddress = null });

            // act
            var products = await this.coinMarketCapClient.GetProductsAsync();

            // assert
            Assert.NotNull(products);
            Assert.True(products.Count() == 0);
        }

        private class SuccessHttpMessageHandlerMock : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, 
                CancellationToken cancellationToken)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

                httpResponseMessage.Content = new StringContent("{\n    \"status\": {\n        \"timestamp\": \"2019-07-29T20:38:13.955Z\",\n        \"error_code\": 0,\n        \"error_message\": null,\n        \"elapsed\": 4,\n        \"credit_count\": 1\n    },\n    \"data\": [\n        {\n            \"id\": 4077,\n            \"name\": \"Maya Preferred 223\",\n            \"symbol\": \"MAPR\",\n            \"slug\": \"maya-preferred-223\",\n            \"num_market_pairs\": 4,\n            \"date_added\": \"2019-07-03T00:00:00.000Z\",\n            \"tags\": [],\n            \"max_supply\": null,\n            \"circulating_supply\": null,\n            \"total_supply\": 250000000,\n            \"platform\": {\n                \"id\": 1321,\n                \"name\": \"Ethereum Classic\",\n                \"symbol\": \"ETC\",\n                \"slug\": \"ethereum-classic\",\n                \"token_address\": \"0x498ae746150fa9ade927808f069e287ab67b62b5\"\n            },\n            \"cmc_rank\": 2054,\n            \"last_updated\": \"2019-07-29T20:37:10.000Z\",\n            \"quote\": {\n                \"USD\": {\n                    \"price\": 23801.1842649,\n                    \"volume_24h\": 25192.3670481197,\n                    \"percent_change_1h\": -1.47753,\n                    \"percent_change_24h\": -0.138395,\n                    \"percent_change_7d\": 15.603,\n                    \"market_cap\": null,\n                    \"last_updated\": \"2019-07-29T20:37:10.000Z\"\n                }\n            }\n        },\n        {\n            \"id\": 1691,\n            \"name\": \"Project-X\",\n            \"symbol\": \"NANOX\",\n            \"slug\": \"project-x\",\n            \"num_market_pairs\": 1,\n            \"date_added\": \"2017-05-30T00:00:00.000Z\",\n            \"tags\": [],\n            \"max_supply\": null,\n            \"circulating_supply\": 0.078264,\n            \"total_supply\": 1.13008462,\n            \"platform\": null,\n            \"cmc_rank\": 1850,\n            \"last_updated\": \"2019-07-29T20:38:02.000Z\",\n            \"quote\": {\n                \"USD\": {\n                    \"price\": 22860.3218573,\n                    \"volume_24h\": 23.6677674756188,\n                    \"percent_change_1h\": 0.155942,\n                    \"percent_change_24h\": -3.67689,\n                    \"percent_change_7d\": 16.1079,\n                    \"market_cap\": 1789.1402298397272,\n                    \"last_updated\": \"2019-07-29T20:38:02.000Z\"\n                }\n            }\n        },\n        {\n            \"id\": 93,\n            \"name\": \"42-coin\",\n            \"symbol\": \"42\",\n            \"slug\": \"42-coin\",\n            \"num_market_pairs\": 5,\n            \"date_added\": \"2014-01-14T00:00:00.000Z\",\n            \"tags\": [\n                \"mineable\"\n            ],\n            \"max_supply\": null,\n            \"circulating_supply\": 41.99995429,\n            \"total_supply\": 41.99995429,\n            \"platform\": null,\n            \"cmc_rank\": 1278,\n            \"last_updated\": \"2019-07-29T20:38:01.000Z\",\n            \"quote\": {\n                \"USD\": {\n                    \"price\": 20221.8873965,\n                    \"volume_24h\": 257.392069319036,\n                    \"percent_change_1h\": 1.58893,\n                    \"percent_change_24h\": -6.3522,\n                    \"percent_change_7d\": -8.64367,\n                    \"market_cap\": 849318.346310527,\n                    \"last_updated\": \"2019-07-29T20:38:01.000Z\"\n                }\n            }\n        }\n    ]\n}");

                return Task.FromResult(httpResponseMessage);
            }
        }

        private class BadRequestHttpMessageHandlerMock : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

                httpResponseMessage.Content = new StringContent("{\n    \"status\": {\n        \"timestamp\": \"2019-07-29T20:45:23.471Z\",\n        \"error_code\": 400,\n        \"error_message\": \"\\\"limit\\\" must be larger than or equal to 1\",\n        \"elapsed\": 0,\n        \"credit_count\": 0\n    }\n}");

                return Task.FromResult(httpResponseMessage);
            }
        }
    }
}
