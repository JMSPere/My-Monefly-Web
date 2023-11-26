using Microsoft.AspNetCore.Mvc;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.Transversal.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _service;

        public RegisterController(
            IUserService _service
        ){
            this._service = _service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterHolder model)
        {
            if (ModelState.IsValid)
            {
                UserRegisterViewModel loginResponse = await _service.RegisterUser(new RegisterRequestDto
                {
                    Name = model.Username,
                    Email = model.Email,
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
