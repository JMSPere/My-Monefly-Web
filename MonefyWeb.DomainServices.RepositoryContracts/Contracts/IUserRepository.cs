using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.DomainServices.DomainWebPage
{
    public interface IUserRepository
    {
        Task<UserLoginResponseDto> Login(LoginRequestDto request);
        Task<UserRegisterResponseDto> Register(RegisterRequestDto request);
    }
}
