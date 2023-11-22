using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Transversal.WebModels;
using Newtonsoft.Json;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class AlphaVantage : IAlphaVantage
    {
        public readonly IHttpClientFactory _httpClientFactory;

        public AlphaVantage() { }

        public AlphaVantage(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AlphaVantageResponse> GetCryptoCurrencyData(ECryptoCurrency currencyCode)
        {
            var currencyCodeText = currencyCode.ToString();
            var httpClient = _httpClientFactory.CreateClient();
            //Development and testing URL
            //var queryUrl = "https://my-json-server.typicode.com/JMSPere/mockApiVantage/db";
            //var response = await httpClient.GetAsync(queryUrl);
            //Final URL
            var queryUrl = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol={currencyCodeText}&market=USD&apikey=IRLZBVARVMPQMJ45";
            var response = await httpClient.GetAsync(queryUrl);

            if (response.IsSuccessStatusCode)
            {
                string cryptoJsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<AlphaVantageResponse>(cryptoJsonResponse);

                // Imprimir los valores en la consola
                string formattedJson = JsonConvert.SerializeObject(apiResponse, Formatting.Indented);
                return apiResponse;
            }
            else
            {
                throw new InvalidOperationException("Bad request!");
            }
        }
    }
}
