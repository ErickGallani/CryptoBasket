namespace CryptoBasket.Application.Dtos
{
    public class LinkDto
    {
        public LinkDto(string href, string rel, string type)
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
