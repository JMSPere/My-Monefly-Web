using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class MovementDetailController: Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccountService _application;
        private readonly IMemoryCache _memoryCache;
        private readonly Transversal.Utils.ILogger _logger;

        private readonly string apiVersion = Properties.Resources.ApiVersion;
        private readonly string baseUrl = Properties.Resources.ApiBaseUrl;

        private static readonly Random random = new();

        public MovementDetailController(
            IAccountService _application,
            IHttpClientFactory _httpClientFactory,
            IMemoryCache _memoryCache,
            Transversal.Utils.ILogger _logger
        )
        {
            this._httpClientFactory = _httpClientFactory;
            this._application = _application;
            this._memoryCache = _memoryCache;
            this._logger = _logger;
        }
    
        public async Task<ActionResult> Index()
        {
            MovementDetailViewModel movementDetailViewModel = new();
            if (_memoryCache.TryGetValue<long>("UserId", out var userId) &&
                _memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                movementDetailViewModel = await _application.GetMovementDetailData(userId, accountId);

            } else {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.MovementData = movementDetailViewModel;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
