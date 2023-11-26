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
    }
}
