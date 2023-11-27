using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Models;
using static System.Collections.Specialized.BitVector32;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class MovementDetailController: Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccountService _application;
        private readonly IMemoryCache _memoryCache;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        private static readonly Random random = new Random();

        public MovementDetailController(
            IAccountService _application,
            IHttpClientFactory httpClientFactory,
            IMemoryCache memory
        )
        {
            _httpClientFactory = httpClientFactory;
            this._application = _application;
            _memoryCache = memory;
        }

        public async Task<ActionResult> Index()
        {
            var movementDetailViewModel = new MovementDetailViewModel
            {
                Sections = new List<MovementSectionViewModel>
                {
                    new MovementSectionViewModel
                    {
                        Name = "Section 1",
                        Icon = "icon-1",
                        Movements = new List<MovementViewModel>
                        {
                            new MovementViewModel { DetailInfo = "DetailInfo 1.1" },
                            new MovementViewModel { DetailInfo = "DetailInfo 1.2" }
                        }
                    },
                    new MovementSectionViewModel
                    {
                        Name = "Section 2",
                        Icon = "icon-2",
                        Movements = new List<MovementViewModel>
                        {
                            new MovementViewModel { DetailInfo = "DetailInfo 2.1" },
                            new MovementViewModel { DetailInfo = "DetailInfo 2.2" }
                        }
                    }
                }
            };
            if (_memoryCache.TryGetValue<long>("UserId", out var userId) &&
                _memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                movementDetailViewModel = await _application.GetMovementDetailData(userId, accountId);
            } else {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.MovementDetailViewModel = movementDetailViewModel;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
