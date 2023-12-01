using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Transversal.WebModels;
using Newtonsoft.Json;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class AlphaVantage : IAlphaVantage
    {
        private readonly Transversal.Utils.ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string baseUrlFirstPart = $"https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol=";
        private readonly string baseUrlLastPart = $"&market=USD&apikey=IRLZBVARVMPQMJ45";

        public AlphaVantage() { }

        public AlphaVantage(IHttpClientFactory httpClientFactory, Transversal.Utils.ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<AlphaVantageResponse> GetCryptoCurrencyData(ECryptoCurrency currencyCode)
        {
            try
            {
                var currencyCodeText = currencyCode.ToString();
                var httpClient = _httpClientFactory.CreateClient();
                var queryUrl = $"{baseUrlFirstPart}{currencyCodeText}{baseUrlLastPart}";
                var response = await httpClient.GetAsync(queryUrl);

                string cryptoJsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<AlphaVantageResponse>(cryptoJsonResponse);
                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new AlphaVantageResponse();
            }
        }
    }
}
