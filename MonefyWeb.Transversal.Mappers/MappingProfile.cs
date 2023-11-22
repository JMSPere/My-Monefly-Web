using AutoMapper;
using MonefyWeb.DistributedServices.Models.Models.Accounts;
using MonefyWeb.DistributedServices.Models.Models.Categories;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.DistributedServices.WebApi.Models;
using MonefyWeb.DomainServices.Models.Models;
using MonefyWeb.Infraestructure.Models;

namespace MonefyWeb.Transversal.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountBe, AccountDto>();
            CreateMap<AccountDto, AccountBe>();

            CreateMap<AccountDm, AccountBe>();
            CreateMap<AccountBe, AccountDm>();

            CreateMap<MovementBe, AccountMovementDto>();
            CreateMap<AccountMovementDto, MovementBe>();

            CreateMap<MovementDm, MovementBe>();
            CreateMap<MovementBe, MovementDm>();

            CreateMap<MovementRequestDto, AccountMovementDto>();
            CreateMap<AccountMovementDto, MovementRequestDto>();

            CreateMap<MovementDm, MovementRequestDto>();
            CreateMap<MovementRequestDto, MovementDm>();

            CreateMap<UserBe, UserDto>();
            CreateMap<UserDto, UserBe>();

            CreateMap<UserDm, UserBe>();
            CreateMap<UserBe, UserDm>();

            CreateMap<UserLoginResponseDto, UserLoginResponseBe>();
            CreateMap<UserLoginResponseBe, UserLoginResponseDto>();

            CreateMap<UserDataResponseDto, UserDataResponseBe>();
            CreateMap<UserDataResponseBe, UserDataResponseDto>();

            CreateMap<UserRegisterResponseDto, UserRegisterResponseBe>();
            CreateMap<UserRegisterResponseBe, UserRegisterResponseDto>();

            CreateMap<CategoryDto, CategoryBe>();
            CreateMap<CategoryBe, CategoryDto>();

            CreateMap<CategoryBe, CategoryDm>();
            CreateMap<CategoryDm, CategoryBe>();
        }
    }
}