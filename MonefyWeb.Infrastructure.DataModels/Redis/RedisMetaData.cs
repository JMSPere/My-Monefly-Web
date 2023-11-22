using Newtonsoft.Json;

namespace MonefyWeb.Infrastructure.DataModels.Redis
{
    public class RedisMetaData
    {
        [JsonProperty("1. Digital Currency Code")]
        public string? DigitalCurrencyCode { get; set; }

        [JsonProperty("2. Digital Currency Name")]
        public string? DigitalCurrencyName { get; set; }

        [JsonProperty("3. Market Code")]
        public string? MarketCode { get; set; }

        [JsonProperty("4. Market Name")]
        public string? MarketName { get; set; }
    }
}
