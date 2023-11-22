using Microsoft.AspNetCore.Mvc;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _service;

        public LoginController(
            IUserService _service
        ) {
            this._service = _service;
        }

        public IActionResult Login()
        {
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

                if (loginResponse == null || loginResponse.Status == false)
                    return View();

                 return RedirectToAction("Index", "AccountChart");
            }
            return BadRequest(ModelState);
        }
    }
}