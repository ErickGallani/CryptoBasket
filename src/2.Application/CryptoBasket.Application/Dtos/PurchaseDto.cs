namespace CryptoBasket.Application.Dtos
{
    public class PurchaseDto
    {
        public PurchaseDto() { }

        public PurchaseDto(ProductDto product, double quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        public ProductDto Product { get; set; }

        public double Quantity { get; set; }
    }
}
