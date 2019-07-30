namespace CryptoBasket.Domain.ValueObjects
{
    using System.Collections.Generic;
    using CryptoBasket.Domain.Core.ValueObjects;

    public class Product : ValueObject<Product>
    {
        private Product() { }

        public Product(int id, string name, string symbol, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Symbol = symbol;
            this.Price = price;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Symbol { get; private set; }

        public double Price { get; private set; }

        public override IEnumerable<object> GetValues()
        {
            yield return this.Id;
            yield return this.Name;
            yield return this.Symbol;
            yield return this.Price;
        }
    }
}
