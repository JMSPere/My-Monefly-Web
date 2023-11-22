using MonefyWeb.Infrastructure.DataModels.Response;
using Newtonsoft.Json;

namespace MonefyWeb.Utils.ServiceAgents
{
    public class Serialization
    {
        public static string JsonSerialize(AlphaVantageResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            string formattedJson = JsonConvert.SerializeObject(response, Formatting.Indented);

            return formattedJson;
        }

        public static List<AlphaVantageResponse> JsonDeserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }
            var response = new List<AlphaVantageResponse>();
            foreach (var item in json.Split("\n"))
            {
                response.Add(JsonConvert.DeserializeObject<AlphaVantageResponse>(item));
            }

            return response;
        }
    }
}
