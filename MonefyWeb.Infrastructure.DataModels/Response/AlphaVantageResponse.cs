using Newtonsoft.Json;

namespace MonefyWeb.Infrastructure.DataModels.Response
{
    public class AlphaVantageResponse
    {
        [JsonProperty("Meta Data")]
        public MetaData? MetaData { get; set; }
        [JsonProperty("Time Series (Digital Currency Daily)")]
        public Dictionary<string, TimeSeriesData>? TimeSeries { get; set; }
    }
}
