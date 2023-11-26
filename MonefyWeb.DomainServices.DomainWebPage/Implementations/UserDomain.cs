using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<UserLoginResponseDto> Login(LoginRequestDto request)
        {
            return await _userRepository.Login(request);
        }

        public async Task<UserRegisterResponseDto> Register(RegisterRequestDto request)
        {
            return await _userRepository.Register(request);
        }
    }
}
