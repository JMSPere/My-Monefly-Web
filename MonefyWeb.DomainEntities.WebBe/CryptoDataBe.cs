namespace MonefyWeb.DomainEntities.WebBe
{
    public class CryptoDataBe
    {
        public int Id { get; set; }
        public string? CurrencyCode { get; set; }
        public string? CurrencyName { get; set; }
        public decimal OpenValue { get; set; }
        public decimal CloseValue { get; set; }
        public string? MarketCode { get; set; }
        public decimal CurrencyChanged { get; set; }
        public bool PriceHasRisen { get; set; }
        public decimal Price { get; set; }
    }
}
