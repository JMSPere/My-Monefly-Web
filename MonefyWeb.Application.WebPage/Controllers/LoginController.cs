using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _service;
        private readonly IMemoryCache _memory;

        public LoginController(
            IUserService _service,
            IMemoryCache _memory
        ) {
            this._service = _service;
            this._memory = _memory;
        }

        public IActionResult Login()
        {
            _memory.Set("UserId", 1L);
            _memory.Set("AccountId", 1L);
            _memory.Set("SessionToken", "tokensillu");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginHolder model)
        {
            if (ModelState.IsValid)
            {
                UserLoginViewModel loginResponse = await _service.LoginUser(new LoginRequestDto
                {
                    Name = model.Username,
                    Password = model.Password
                });

                if (loginResponse == null
                    || loginResponse.Status == false)
                    return RedirectToAction("Login", "Login");

                _memory.Set("UserId", loginResponse.UserId);
                _memory.Set("AccountId", loginResponse.AccountId);
                _memory.Set("SessionToken", loginResponse.Token);

                return RedirectToAction("Index", "AccountChart");
            }
            return BadRequest(ModelState);
        }
    }
}