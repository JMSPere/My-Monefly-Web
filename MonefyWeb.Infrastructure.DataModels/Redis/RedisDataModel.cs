using Newtonsoft.Json;

namespace MonefyWeb.Infrastructure.DataModels.Redis
{
    public class RedisDataModel
    {
        [JsonProperty("Redis Dictionary (Digital Currency Daily)")]
        public Dictionary<RedisMetaData, RedisCryptoData>? CryptoEntry { get; set; }
    }
}
