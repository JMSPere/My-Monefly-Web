using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.DomainServices.DomainWebPage;
using MonefyWeb.DomainServices.RepositoryContracts.Contracts;
using MonefyWeb.Infraestructure.RepositoryWebPage.Implementations;
using MonefyWeb.Transversal.Models;
using MonefyWeb.Transversal.Utils.Token;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTools_NTS.Util;

namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IApiServiceAgent _service;
        private readonly IMemoryCache _memory;
        private readonly ITokenUtils _token;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public UserRepository(IApiServiceAgent _service, ITokenUtils _token, IMemoryCache memory)
        {
            this._service = _service;
            this._token = _token;
            this._memory = memory;
        }

        public async Task<UserLoginResponseDto> Login(LoginRequestDto request)
        {
            var response = new UserLoginResponseDto();
            var PostLoginUser = $"{baseUrl}{apiVersion}/User/Login";

            var PostLoginResponse = await _service.PostApiAsync(PostLoginUser, string.Empty, request);

            if (PostLoginResponse != null)
            {
                response = JsonConvert.DeserializeObject<UserLoginResponseDto>(PostLoginResponse);
            }

            return response;
        }

        public async Task<UserRegisterResponseDto> Register(RegisterRequestDto request)
        {
            var response = new UserRegisterResponseDto();
            var PostRegisterUser = $"{baseUrl}{apiVersion}/User/Register";
            if (_memory.TryGetValue<string>("SessionToken", out var token))
            {
                var PostRegisterUserResponse = await _service.PostApiAsync(PostRegisterUser, token, request);

                if (PostRegisterUserResponse != null)
                {
                    response = JsonConvert.DeserializeObject<UserRegisterResponseDto>(PostRegisterUserResponse);
                }
            }
            else
            {
                throw new InvalidTokenException();
            }
            return response;
        }
    }
}
