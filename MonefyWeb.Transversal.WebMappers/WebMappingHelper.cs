using MonefyWeb.DomainEntities.WebBe;
using MonefyWeb.Infrastructure.DataModels.Redis;
using MonefyWeb.Infrastructure.DataModels.Response;

namespace MonefyWeb.Transversal.WebMappers
{
    public class WebMappingHelper
    {
        public static CryptoDataBe MapToCryptoDataBe(AlphaVantageResponse model)
        {
            return new CryptoDataBe
            {
                Id = 0,
                CurrencyCode = model.MetaData.DigitalCurrencyCode,
                CurrencyName = model.MetaData.DigitalCurrencyName,
                OpenValue = Math.Round(decimal.Parse(model.TimeSeries.First().Value.OpenUSD), 2),
                CloseValue = Math.Round(decimal.Parse(model.TimeSeries.First().Value.CloseUSD), 2),
                MarketCode = model.MetaData.MarketCode,
                CurrencyChanged = 0,
                PriceHasRisen = false,
                Price = Math.Abs(Math.Round(decimal.Parse(model.TimeSeries.First().Value.CloseUSD), 2) - Math.Round(decimal.Parse(model.TimeSeries.Last().Value.OpenUSD), 2))
            };
        }

        //mapper from alphaVantageResponse to RedisDataModel
        public static RedisDataModel MapToRedisCryptoData(AlphaVantageResponse model)
        {
            return new RedisDataModel
            {
                CryptoEntry = new Dictionary<RedisMetaData, RedisCryptoData>
                {
                    {
                        new RedisMetaData
                        {
                            DigitalCurrencyCode = model.MetaData.DigitalCurrencyCode,
                            DigitalCurrencyName = model.MetaData.DigitalCurrencyName,
                            MarketCode = model.MetaData.MarketCode,
                            MarketName = model.MetaData.MarketName
                        },
                        new RedisCryptoData
                        {
                            OpenUSD = model.TimeSeries.First().Value.OpenUSD,
                            HighUSD = model.TimeSeries.First().Value.HighUSD,
                            LowUSD = model.TimeSeries.First().Value.LowUSD,
                            CloseUSD = model.TimeSeries.First().Value.CloseUSD,
                            Volume = model.TimeSeries.First().Value.Volume,
                            MarketCapUSD = model.TimeSeries.First().Value.MarketCapUSD
                        }
                    }
                }
            };
        }
    }
}
