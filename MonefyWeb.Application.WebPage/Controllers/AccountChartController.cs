using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Models;
using MonefyWeb.DistributedServices.Models.Models.Accounts;
using MonefyWeb.DistributedServices.Models.Models.Categories;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.Transversal.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class AccountChartController : Controller
    {
        private readonly ILogger<AccountChartController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApplicationWebPage _application;
        private readonly IMemoryCache _memoryCache;

        private readonly string apiVersion = "v2";


        private static readonly Random random = new Random();

        public AccountChartController(
            ILogger<AccountChartController> logger,
            IApplicationWebPage _application,
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
                var chartPoco = _application.GetChartData(userId, accountId);
                
                var httpClient = _httpClientFactory.CreateClient();
                var GetCategories = $"https://localhost:7006/api/{apiVersion}/Category/GetCategoriesByUserId?UserId={userId}";
                var PostMovements = $"https://localhost:7006/api/{apiVersion}/Account/GetMovementsByAccountId?AccountId={accountId}";

                var GetCategoriesResponse = await httpClient.GetAsync(GetCategories);
                var PostMovementsResponse = await httpClient.PostAsync(PostMovements, null);


                if (GetCategoriesResponse.IsSuccessStatusCode && PostMovementsResponse.IsSuccessStatusCode)
                {
                    var categoriesBe = JsonConvert.DeserializeObject<List<CategoryDto>>(await GetCategoriesResponse.Content.ReadAsStringAsync());
                    var movementsBe = JsonConvert.DeserializeObject<List<AccountMovementDto>>(await PostMovementsResponse.Content.ReadAsStringAsync());

                    List<object> chartData = new List<object>();
                    var categories = new List<KeyValuePair<string, string>>();
                    var cartDataColor = new List<KeyValuePair<string, string>>();
                    var incomeCategories = new List<KeyValuePair<long, string>>();
                    var expenseCategories = new List<KeyValuePair<long, string>>();

                    decimal totalIncomes = movementsBe
                        .Where(m => m.Type == EMovementType.Add)
                        .Sum(m => m.Amount);

                    decimal totalExpenses = movementsBe
                        .Where(m => m.Type == EMovementType.Substract)
                        .Sum(m => m.Amount);

                    ViewBag.Balance = totalIncomes - totalExpenses;

                    foreach (var category in categoriesBe)
                    {
                        var totalAmount = movementsBe
                            .Where(m => m.CategoryId == category.Id && m.Type == EMovementType.Substract)
                            .Sum(m => m.Amount);

                        string randomColor = GenerateRandomColor();
                        chartData.Add(new
                        {
                            name = category.Name,
                            totalAmount = totalAmount,
                            color = randomColor
                        });

                        categories.Add(new KeyValuePair<string, string>(category.Name, category.Icon));

                        if (category.Type == 2) { expenseCategories.Add(new KeyValuePair<long, string>(category.Id, category.Name)); }
                        if (category.Type == 1) { incomeCategories.Add(new KeyValuePair<long, string>(category.Id, category.Name)); }
                    }

                    ViewBag.Incomes = totalIncomes;
                    ViewBag.Expenses = totalExpenses;
                    ViewBag.ChartData = JsonConvert.SerializeObject(chartData);
                    ViewBag.Categories = categories;
                    ViewBag.IncomeCategories = incomeCategories;
                    ViewBag.ExpenseCategories = expenseCategories;

                    return View();
                }
                else if (GetCategoriesResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await GetCategoriesResponse.Content.ReadAsStringAsync();
                    return BadRequest(errorContent);
                }
                else
                {
                    return StatusCode((int)GetCategoriesResponse.StatusCode);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public static string GenerateRandomColor()
        {
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            int brightnessThreshold = 384;
            while (colorBytes[0] + colorBytes[1] + colorBytes[2] < brightnessThreshold)
            {
                random.NextBytes(colorBytes);
            }

            string randomColor = $"#{colorBytes[0]:X2}{colorBytes[1]:X2}{colorBytes[2]:X2}";

            return randomColor;
        }

        [HttpPost]
        public async Task<IActionResult> AddIncome(TemporalHelperModel model)
        {
            if (_memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                var httpClient = _httpClientFactory.CreateClient();

                var movement = new MovementRequestDto()
                {
                    AccountId = accountId,
                    Amount = model.IncomeAmount,
                    CategoryId = model.IncomeCategory,
                    Concept = "Income",
                    PaymentMethod = EPaymentMethod.Cash,
                    Type = EMovementType.Add
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(movement), Encoding.UTF8, "application/json");

                var PostMovements = $"https://localhost:7006/api/{apiVersion}/Account/AddMovementToAccount";

                var addMovementToAccountResponse = await httpClient.PostAsync(PostMovements, jsonContent);
                
                if (addMovementToAccountResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("The API request has failed!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubstractExpense(TemporalHelperModel model)
        {
            if (_memoryCache.TryGetValue<long>("AccountId", out var accountId))
            {
                var httpClient = _httpClientFactory.CreateClient();

                var movement = new MovementRequestDto()
                {
                    AccountId = accountId,
                    Amount = model.ExpenseAmount,
                    CategoryId = model.ExpenseCategory,
                    Concept = "Expense",
                    PaymentMethod = EPaymentMethod.Cash,
                    Type = EMovementType.Substract
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(movement), Encoding.UTF8, "application/json");

                var GetMovements = $"https://localhost:7006/api/{apiVersion}/Account/AddMovementToAccount";
                var addMovementToAccountResponse = await httpClient.PostAsync(GetMovements, jsonContent);

                if (addMovementToAccountResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("The API request has failed!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
