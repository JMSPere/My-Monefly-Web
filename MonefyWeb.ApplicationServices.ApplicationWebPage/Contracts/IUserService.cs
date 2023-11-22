using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public interface IUserService
    {
        Task<UserLoginViewModel> LoginUser(LoginRequestDto request);
        Task<UserRegisterViewModel> RegisterUser(RegisterRequestDto request);
    }
}