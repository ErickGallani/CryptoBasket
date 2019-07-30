using CryptoBasket.Application.Interfaces;
using Moq;

namespace CryptoBasket.Api.Tests.Controllers
{
    public class PurchaseControllerTests
    {
        private readonly Mock<IPurchaseService> purchaseService;

        public PurchaseControllerTests()
        {
            this.purchaseService = new Mock<IPurchaseService>();
        }
    }
}
