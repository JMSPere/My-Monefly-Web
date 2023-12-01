using MonefyWeb.DomainEntities.WebBe;
using MonefyWeb.DomainServices.DomainWebPage.Contracts;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Transversal.Aspects;
using MonefyWeb.Transversal.WebMappers;
using MonefyWeb.Transversal.WebModels;
using Newtonsoft.Json;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class CryptoLogic : ICryptoLogic
    {
        private readonly IAlphaVantage _alphaVantage;
        private readonly IRedisCache _redisCache;
        private readonly Transversal.Utils.ILogger _logger;

        [Log]
        public CryptoLogic(IAlphaVantage alphaVantage, IRedisCache redisCache, Transversal.Utils.ILogger _logger)
        {
            _alphaVantage = alphaVantage;
            _redisCache = redisCache;
            this._logger = _logger;
        }

        [Log]
        public TimeSeriesEntry? GetTimeSeriesByDay(DateTime day, AlphaVantageResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException();
            }
            var key = day.ToString(Properties.Resources.DatePattern);
            if (response.TimeSeries.ContainsKey(key))
            {
                return new TimeSeriesEntry
                {
                    Date = key,
                    SeriesData = response.TimeSeries[key]
                };
            }
            else
            {
                return null;
            }
        }

        [Log]
        public AlphaVantageResponse GetLastTwoDays(AlphaVantageResponse response)
        {
            try
            {
                if (response.TimeSeries == null)
                {
                    return null;
                }

                int i = 1;
                var todayTimeSeries = GetTimeSeriesByDay(DateTime.Today, response);
                while (todayTimeSeries == null)
                {
                    todayTimeSeries = GetTimeSeriesByDay(DateTime.Today.AddDays(-i), response);
                    i++;
                }

                var yesterdayTimeSeries = GetTimeSeriesByDay(DateTime.Today.AddDays(-i), response);
                while (yesterdayTimeSeries == null)
                {
                    i++;
                    yesterdayTimeSeries = GetTimeSeriesByDay(DateTime.Today.AddDays(-i), response);
                }

                var apiResponseLastTwoDays = new AlphaVantageResponse
                {
                    MetaData = response.MetaData,
                    TimeSeries = new Dictionary<string, TimeSeriesData>()
                };

                if (todayTimeSeries != null)
                {
                    apiResponseLastTwoDays.TimeSeries.Add(todayTimeSeries.Date, todayTimeSeries.SeriesData);
                }

                if (yesterdayTimeSeries != null)
                {
                    apiResponseLastTwoDays.TimeSeries.Add(yesterdayTimeSeries.Date, yesterdayTimeSeries.SeriesData);
                }

                return apiResponseLastTwoDays;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TODO separate responsability
        [Log]
        public async Task<List<CryptoDataBe>> GetCryptoData()
        {
            var listAlphaVantage = new List<AlphaVantageResponse>();
            var cryptoDataBeList = new List<CryptoDataBe>();

            if (await _redisCache.HasData())
            {
                listAlphaVantage = await _redisCache.GetAllAsyncJson();

                foreach (var alphaVantageData in listAlphaVantage)
                {
                    cryptoDataBeList.Add(WebMappingHelper.MapToCryptoDataBe(alphaVantageData));
                    cryptoDataBeList.Last().PriceHasRisen = HasPriceRisen(alphaVantageData);
                    cryptoDataBeList.Last().CurrencyChanged = Math.Round(ChangePercentage(alphaVantageData), 2);
                }
            }
            else
            {
                for (int i = 0; i <= 9; i++)
                {
                    var alphaVantageResponse = await _alphaVantage.GetCryptoCurrencyData(ECryptoCurrency.BTC + i);
                    var lastTwoDays = GetLastTwoDays(alphaVantageResponse);
                    if (lastTwoDays == null)
                    {
                        continue;
                    }
                    _redisCache.SetAsyncJson(lastTwoDays.MetaData.DigitalCurrencyCode, JsonConvert.SerializeObject(lastTwoDays));
                    cryptoDataBeList.Add(WebMappingHelper.MapToCryptoDataBe(lastTwoDays));
                    cryptoDataBeList.Last().PriceHasRisen = HasPriceRisen(lastTwoDays);
                    cryptoDataBeList.Last().CurrencyChanged = Math.Round(ChangePercentage(lastTwoDays), 2);
                }
            }
            cryptoDataBeList = cryptoDataBeList.OrderByDescending(o => o.CurrencyChanged).ToList();
            //TODO erase minus sign for percentages in frontend and eliminate this loop
            foreach (var cryptoDataBe in cryptoDataBeList)
            {
                cryptoDataBe.CurrencyChanged = Math.Abs(cryptoDataBe.CurrencyChanged);
            }

            return cryptoDataBeList.OrderByDescending(o => o.CurrencyChanged).ToList();
        }

        [Log]
        public bool HasPriceRisen(AlphaVantageResponse alphaVantageResponse)
        {
            var lastTwoDays = GetLastTwoDays(alphaVantageResponse);

            if (lastTwoDays == null)
            {
                return false;
            }

            var lastDay = lastTwoDays.TimeSeries.FirstOrDefault();
            var previousDay = lastTwoDays.TimeSeries.LastOrDefault();

            if (decimal.Parse(lastDay.Value.CloseUSD) > decimal.Parse(previousDay.Value.CloseUSD))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Log]
        public decimal ChangePercentage(AlphaVantageResponse alphaVantageResponse)
        {
            try
            {
                var lastTwoDays = GetLastTwoDays(alphaVantageResponse);

                if (lastTwoDays == null)
                {
                    return 0;
                }

                var lastDay = lastTwoDays.TimeSeries.First();
                var previousDay = lastTwoDays.TimeSeries.Last();

                var lastDayClose = decimal.Parse(lastDay.Value.CloseUSD);
                var previousDayClose = decimal.Parse(previousDay.Value.CloseUSD);

                var changePercentage = (lastDayClose - previousDayClose) / previousDayClose * 100;

                return changePercentage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
