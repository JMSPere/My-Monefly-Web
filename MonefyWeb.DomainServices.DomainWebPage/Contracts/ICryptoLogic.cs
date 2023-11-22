using MonefyWeb.DomainEntities.WebBe;
using MonefyWeb.Infrastructure.DataModels.Response;

namespace MonefyWeb.DomainServices.DomainWebPage.Contracts
{
    public interface ICryptoLogic
    {
        TimeSeriesEntry? GetTimeSeriesByDay(DateTime day, AlphaVantageResponse response);
        AlphaVantageResponse GetLastTwoDays(AlphaVantageResponse response);
        Task<List<CryptoDataBe>> GetCryptoData();
        bool HasPriceRisen(AlphaVantageResponse alphaVantageResponse);
        decimal ChangePercentage(AlphaVantageResponse alphaVantageResponse);
    }
}
