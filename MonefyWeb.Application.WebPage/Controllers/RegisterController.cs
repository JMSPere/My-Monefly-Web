using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DistributedServices.Models.Models.Users;
using Newtonsoft.Json;
using System.Text;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IEncryptUtils _encrypt;

        private readonly string apiVersion = "v2";

        public RegisterController(
            ILogger<RegisterController> logger,
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache,
            IEncryptUtils _encrypt)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            this._encrypt = _encrypt;
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
                var httpClient = _httpClientFactory.CreateClient();
                var registerUrl = $"https://localhost:7006/api/{apiVersion}/User/Register";

                var requestBody = new
                {
                    name = model.Username,
                    password = _encrypt.ComputeSHA256Hash(model.Password),
                    email = model.Email,
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(registerUrl, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var registerResponse = JsonConvert.DeserializeObject<UserRegisterResponseDto>(jsonResponse);


                if (response.IsSuccessStatusCode && registerResponse.Status == true)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View("Error");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
