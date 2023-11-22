using Newtonsoft.Json;

namespace MonefyWeb.Infrastructure.DataModels.Response
{
    public class MetaData
    {
        [JsonProperty("1. Information")]
        public string? Information { get; set; }

        [JsonProperty("2. Digital Currency Code")]
        public string? DigitalCurrencyCode { get; set; }

        [JsonProperty("3. Digital Currency Name")]
        public string? DigitalCurrencyName { get; set; }

        [JsonProperty("4. Market Code")]
        public string? MarketCode { get; set; }

        [JsonProperty("5. Market Name")]
        public string? MarketName { get; set; }

        [JsonProperty("6. Last Refreshed")]
        public string? LastRefreshed { get; set; }

        [JsonProperty("7. Time Zone")]
        public string? TimeZone { get; set; }
    }
}
