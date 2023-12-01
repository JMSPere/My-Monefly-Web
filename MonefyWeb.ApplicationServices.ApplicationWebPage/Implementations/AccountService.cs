using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Movements;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDomainWebPage _domain;
        private readonly IMapper _mapper;

        public AccountService(IAccountDomainWebPage _domain, IMapper mapper)
        {
            this._domain = _domain;
            this._mapper = mapper;
        }

        public async Task<AddMovementResponseViewModel> AddMovement(MovementRequestDto movementRequestDto)
        {
            return _mapper.Map<AddMovementResponseViewModel>(await _domain.AddMovement(movementRequestDto));
        }

        public async Task<ChartDataViewModel> GetChartData(long UserId, long AccountId)
        {
            return await _domain.GetChartData(UserId, AccountId);
        }

        public async Task<MovementDetailViewModel> GetMovementDetailData(long userId, long accountId)
        {
            var result = await _domain.GetMovementDetailData(userId, accountId);

            var groupedMovements = result.GroupBy(r => r.CategoryName);

            var sections = new List<MovementSectionViewModel>();
            foreach (var group in groupedMovements)
            {
                var section = new MovementSectionViewModel
                {
                    Name = group.First().CategoryName,
                    Icon = string.Empty,
                    Movements = group.Select(item => new MovementViewModel
                    {
                        DetailInfo = $"{item.Concept} on {item.MovementDate.Day}/{item.MovementDate.Month}/{item.MovementDate.Year} - {item.Amount:C} using a {item.PaymentMethod}"
                    }).ToList()
                };
                sections.Add(section);
            }

            var viewModel = new MovementDetailViewModel
            {
                Sections = sections
            };

            return viewModel;
       }

    }
}
