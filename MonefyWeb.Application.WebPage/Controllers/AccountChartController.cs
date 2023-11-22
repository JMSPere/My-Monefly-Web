using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.Transversal.Models;
using Newtonsoft.Json;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class AccountChartController : Controller
    {
        private readonly ILogger<AccountChartController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccountService _application;
        private readonly IMemoryCache _memoryCache;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        private static readonly Random random = new Random();

        public AccountChartController(
            ILogger<AccountChartController> logger,
            IAccountService _application,
            IHttpClientFactory httpClientFactory,
            IMemoryCache memory
        )
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            this._application = _application;
            _memoryCache = memory;
        }

        public async Task<ActionResult> Index()
        {
            if (_memoryCache.TryGetValue<long>("UserId", out var userId) &&
                _memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                var chartPoco = await _application.GetChartData(userId, accountId);

                ViewBag.Incomes = chartPoco.TotalIncomes;
                ViewBag.Expenses = chartPoco.TotalExpenses;
                ViewBag.ChartData = JsonConvert.SerializeObject(chartPoco.ChartData);
                ViewBag.Categories = chartPoco.Categories;
                ViewBag.IncomeCategories = chartPoco.IncomeCategories;
                ViewBag.ExpenseCategories = chartPoco.ExpenseCategories;

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddIncome(TemporalHelperModel model)
        {
            if (_memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                await _application.AddMovement(new MovementRequestDto()
                {
                    AccountId = accountId,
                    Amount = model.IncomeAmount,
                    CategoryId = model.IncomeCategory,
                    Concept = model.Concept,
                    PaymentMethod = EPaymentMethod.Cash,
                    Type = EMovementType.Add
                });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubstractExpense(TemporalHelperModel model)
        {
            if (_memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                await _application.AddMovement(new MovementRequestDto()
                {
                    AccountId = accountId,
                    Amount = model.IncomeAmount,
                    CategoryId = model.IncomeCategory,
                    Concept = model.Concept,
                    PaymentMethod = EPaymentMethod.Cash,
                    Type = EMovementType.Substract
                });
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
