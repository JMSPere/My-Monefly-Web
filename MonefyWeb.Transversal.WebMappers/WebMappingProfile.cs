using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.DomainEntities.WebBe;
using MonefyWeb.Infrastructure.DataModels.Redis;
using MonefyWeb.Infrastructure.DataModels.Response;

namespace MonefyWeb.Transversal.WebMappers
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CryptoDataBe, CryptoDataResponse>().ReverseMap();

            CreateMap<TimeSeriesData, RedisCryptoData>().ReverseMap();
        }
    }
}
