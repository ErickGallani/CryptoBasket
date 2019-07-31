namespace CryptoBasket.Api.Models
{
    public class Link
    {
        public Link(string href, string rel, string type)
        {
            this.Href = href;
            this.Rel = rel;
            this.Type = type;
        }

        public string Href { get; set; }

        public string Rel { get; set; }

        public string Type { get; set; }
    }
}
