using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.DomainEntities.WebBe;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infrastructure.DataModels.Redis;
using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Transversal.WebMappers
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CryptoDataBe, CryptoDataResponse>().ReverseMap();

            CreateMap<TimeSeriesData, RedisCryptoData>().ReverseMap();

            CreateMap<ChartDataDto, ChartDataViewModel>().ReverseMap();

            CreateMap<AddMovementResponseBe, AddMovementResponseDto>().ReverseMap();

            CreateMap<AddMovementResponseBe, AddMovementResponseViewModel>().ReverseMap();

            CreateMap<LoginRequestDto, UserLoginBe>().ReverseMap();

            CreateMap<UserLoginBe, UserLoginViewModel>().ReverseMap();

            CreateMap<UserLoginResponseDto, UserLoginViewModel>().ReverseMap();

            CreateMap<UserRegisterResponseDto, UserRegisterViewModel>().ReverseMap();

            CreateMap<MovementDetailViewModel, MovementDetailBe>().ReverseMap();

            CreateMap<MovementDetailBe, MovementDetailDto>().ReverseMap();

        }
    }
}
