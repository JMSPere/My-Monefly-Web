using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Transversal.WebModels;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public interface IAlphaVantage
    {
        Task<AlphaVantageResponse> GetCryptoCurrencyData(ECryptoCurrency currencyCode);
    }
}
