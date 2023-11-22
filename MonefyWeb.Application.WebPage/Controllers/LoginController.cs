using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Users;
using Newtonsoft.Json;
using System.Text;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IEncryptUtils _encrypt;

        private readonly string apiVersion = "v2";

        public LoginController(
            ILogger<LoginController> logger,
            IHttpClientFactory httpClientFactory,
            IMemoryCache memoryCache,
            IEncryptUtils _encrypt)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            this._encrypt = _encrypt;
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
                string hashedPassword = _encrypt.ComputeSHA256Hash(model.Password);

                var httpClient = _httpClientFactory.CreateClient();
                var loginUrl = $"https://localhost:7006/api/{apiVersion}/User/Login";

                var requestBody = new
                {
                    name = model.Username,
                    password = hashedPassword
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(loginUrl, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var loginResponse = JsonConvert.DeserializeObject<UserLoginResponseDto>(jsonResponse);

                if (response.IsSuccessStatusCode && loginResponse.Status == true)
                {
                    _memoryCache.Set("UserId", loginResponse.UserId);
                    _memoryCache.Set("AccountId", loginResponse.AccountId);
                    return RedirectToAction("Index", "AccountChart");
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