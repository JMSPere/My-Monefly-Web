using Newtonsoft.Json;
namespace MonefyWeb.Infrastructure.DataModels.Response
{
    public class TimeSeriesData
    {
        [JsonProperty("1b. open (USD)")]
        public string? OpenUSD { get; set; }

        [JsonProperty("2b. high (USD)")]
        public string? HighUSD { get; set; }

        [JsonProperty("3b. low (USD)")]
        public string? LowUSD { get; set; }

        [JsonProperty("4b. close (USD)")]
        public string? CloseUSD { get; set; }

        [JsonProperty("5. volume")]
        public string? Volume { get; set; }

        [JsonProperty("6. market cap (USD)")]
        public string? MarketCapUSD { get; set; }
    }
}
