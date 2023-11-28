using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DistributedServices.Models.Models.Accounts;
using MonefyWeb.DistributedServices.Models.Models.Categories;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.DomainServices.RepositoryContracts.Contracts;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Transversal.Models;
using MonefyWeb.Transversal.Utils.Chart;
using Newtonsoft.Json;
using System;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IApiServiceAgent _service;
        private readonly IMemoryCache _memory;
        private readonly IChartUtils _chart;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public AccountRepository(IApiServiceAgent apiServiceAgent, IMemoryCache _memory, IChartUtils _chart)
        {
            this._service = apiServiceAgent;
            this._memory = _memory;
            this._chart = _chart;
        }

        public async Task<ChartDataDto> GetChartData(long UserId, long AccountId)
        {
            var result = new ChartDataDto();
            var GetCategories = $"{baseUrl}{apiVersion}/Category/GetCategoriesByUserId?UserId={UserId}";
            var PostMovements = $"{baseUrl}{apiVersion}/Account/GetMovementsByAccountId?AccountId={AccountId}";

            if (_memory.TryGetValue<string>("SessionToken", out var token))
            {
                var GetCategoriesResponse = await _service.GetApiAsync(GetCategories, token);
                var PostMovementsResponse = await _service.PostApiAsync(PostMovements, token, null);


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

                        string randomColor = _chart.GenerateRandomColor();
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

                } else
                {
                    throw new InvalidTokenException(nameof(AccountRepository));
                }
            }
            return result;
        }


        public async Task<AddMovementResponseDto> AddMovement(MovementRequestDto movementRequestDto)
        {
            var result = new AddMovementResponseDto();
            var AddMovementToAccount = $"{baseUrl}{apiVersion}/Account/AddMovementToAccount";
            if (_memory.TryGetValue<string>("SessionToken", out var token))
            {
                var PostMovementsResponse = await _service.PostApiAsync(AddMovementToAccount, token, movementRequestDto);

                if (PostMovementsResponse != null)
                {
                    var movementResponse = JsonConvert.DeserializeObject<AddMovementResponseDto>(PostMovementsResponse);
                    result.Status = movementResponse.Status;
                }
            }
            else
            {
                throw new InvalidTokenException();
            }
            return result;
        }

        public async Task<List<MovementDetailDto>> GetMovementDetailData(long userId, long accountId)
        {
            var result = new List<MovementDetailDto>();
            var movementDetailPoco = new GetMovementAccountPoco { AccountId = accountId };
            var movementDetalUrl = $"{baseUrl}{apiVersion}/Account/GetMovementDetailData?AccountId={accountId}";
            if (_memory.TryGetValue<string>("SessionToken", out var token))
            {
                var PostMovementDetalResponse = await _service.GetApiAsync(movementDetalUrl, token);

                if (PostMovementDetalResponse != null)
                {
                    result = JsonConvert.DeserializeObject<List<MovementDetailDto>>(PostMovementDetalResponse);
                }
            }
            else
            {
                throw new InvalidTokenException();
            }
            return result;
        }
    }
}
