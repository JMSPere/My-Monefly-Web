using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DistributedServices.Models.Models.Accounts;
using MonefyWeb.DistributedServices.Models.Models.Categories;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.DomainServices.RepositoryContracts.Contracts;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Transversal.Models;
using Newtonsoft.Json;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IApiServiceAgent _service;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public AccountRepository(IApiServiceAgent apiServiceAgent)
        {
            this._service = apiServiceAgent;
        }

        public async Task<ChartDataDto> GetChartData(long UserId, long AccountId)
        {
            var result = new ChartDataDto();
            var GetCategories = $"{baseUrl}{apiVersion}/Category/GetCategoriesByUserId?UserId={UserId}";
            var PostMovements = $"{baseUrl}{apiVersion}/Account/GetMovementsByAccountId?AccountId={AccountId}";

            var GetCategoriesResponse = await _service.GetApiAsync(GetCategories);
            var PostMovementsResponse = await _service.PostApiAsync(PostMovements, null);


            if (GetCategoriesResponse != null && PostMovementsResponse != null)
            {
                var categoriesBe = JsonConvert.DeserializeObject<List<CategoryDto>>(GetCategoriesResponse);
                var movementsBe = JsonConvert.DeserializeObject<List<AccountMovementDto>>(PostMovementsResponse);

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

                result.Balance = totalIncomes - totalExpenses;

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

                result.TotalIncomes = totalIncomes;
                result.TotalExpenses = totalExpenses;
                result.ChartData = chartData;
                result.Categories = categories;
                result.IncomeCategories = incomeCategories;
                result.ExpenseCategories = expenseCategories;

            }
            return result;
        }
        public static string GenerateRandomColor()
        {
            byte[] colorBytes = new byte[3];
            Random random = new Random();
            random.NextBytes(colorBytes);

            int brightnessThreshold = 384;
            while (colorBytes[0] + colorBytes[1] + colorBytes[2] < brightnessThreshold)
            {
                random.NextBytes(colorBytes);
            }

            string randomColor = $"#{colorBytes[0]:X2}{colorBytes[1]:X2}{colorBytes[2]:X2}";

            return randomColor;
        }

        public async Task<AddMovementResponseDto> AddMovement(MovementRequestDto movementRequestDto)
        {
            var result = new AddMovementResponseDto();
            var AddMovementToAccount = $"{baseUrl}{apiVersion}/Account/AddMovementToAccount";

            var PostMovementsResponse = await _service.PostApiAsync(AddMovementToAccount, movementRequestDto);

            if (PostMovementsResponse != null)
            {
                var movementResponse = JsonConvert.DeserializeObject<AddMovementResponseDto>(PostMovementsResponse);
                result.Status = movementResponse.Status;
            }
            return result;
        }

        public async Task<MovementDetailDto> GetMovementDetailData(long userId, long accountId)
        {
            var result = new MovementDetailDto();
            var movementDetailPoco = new GetMovementAccountPoco { UserId = userId, AccountId = accountId };
            var movementDetalUrl = $"{baseUrl}{apiVersion}/Account/GetMovementDetailData";

            var PostMovementDetalResponse = await _service.PostApiAsync(movementDetalUrl, movementDetailPoco);

            if (PostMovementDetalResponse != null)
            {
                var movementResponse = JsonConvert.DeserializeObject<MovementDetailDto>(PostMovementDetalResponse);
            }
            return result;
        }
    }
}
