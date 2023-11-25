using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.DistributedServices.Models.Models.Users;
using MonefyWeb.DomainServices.DomainWebPage;
using MonefyWeb.DomainServices.RepositoryContracts.Contracts;
using MonefyWeb.Transversal.Models;
using Newtonsoft.Json;

namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IApiServiceAgent _service;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public UserRepository(IApiServiceAgent service)
        {
            _service = service;
        }

        public async Task<UserLoginResponseDto> Login(LoginRequestDto request)
        {
            var response = new UserLoginResponseDto();
            var PostLoginUser = $"{baseUrl}{apiVersion}/User/Login";

            var PostRegisterResponse = await _service.PostApiAsync(PostLoginUser, request);

            if (PostRegisterResponse != null)
            {
                response = JsonConvert.DeserializeObject<UserLoginResponseDto>(PostRegisterResponse);
            }
            return response;
        }

        public async Task<UserRegisterResponseDto> Register(RegisterRequestDto request)
        {
            var response = new UserRegisterResponseDto();
            var PostRegisterUser = $"{baseUrl}{apiVersion}/User/Register";

            var PostRegisterUserResponse = await _service.PostApiAsync(PostRegisterUser, request);

            if (PostRegisterUserResponse != null)
            {
                response = JsonConvert.DeserializeObject<UserRegisterResponseDto>(PostRegisterUserResponse);
            }
            return response;
        }
    }
}
