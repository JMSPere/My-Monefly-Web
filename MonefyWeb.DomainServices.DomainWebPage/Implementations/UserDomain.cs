using AutoMapper;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserDomain(IUserRepository userRepository, IMapper _mapper)
        {
            this._userRepository = userRepository;
            this._mapper = _mapper;
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
