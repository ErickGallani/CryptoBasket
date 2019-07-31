namespace CryptoBasket.Application.Dtos
{
    public class ProductDto
    {
        public ProductDto() { }

        public ProductDto(int id, string name, string symbol, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Symbol = symbol;
            this.Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double Price { get; set; }
    }
}
