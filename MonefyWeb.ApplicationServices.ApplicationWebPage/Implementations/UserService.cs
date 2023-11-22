using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _domain;
        private readonly IMapper _mapper;

        public UserService(IUserDomain _domain, IMapper _mapper)
        {
            this._domain = _domain;
            this._mapper = _mapper;
        }

        public async Task<UserLoginViewModel> LoginUser(LoginRequestDto request)
        {
            var result = _mapper.Map<UserLoginViewModel>(await _domain.Login(request));
            return result;
        }

        public async Task<UserRegisterViewModel> RegisterUser(RegisterRequestDto request)
        {
            var result = _mapper.Map<UserRegisterViewModel>(await _domain.Register(request));
            return result;
        }
    }
}
