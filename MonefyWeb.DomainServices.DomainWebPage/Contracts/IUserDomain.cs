using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface IUserDomain
    {
        Task<UserLoginResponseDto> Login(LoginRequestDto request);
        Task<UserRegisterResponseDto> Register(RegisterRequestDto request);
    }
}