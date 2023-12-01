using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Transversal.Aspects;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class AccountDomainWebPage : IAccountDomainWebPage
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly Transversal.Utils.ILogger _logger;

        public AccountDomainWebPage(IAccountRepository repository, IMapper mapper, Transversal.Utils.ILogger _logger)
        {
            _repository = repository;
            _mapper = mapper;
            this._logger = _logger;
        }

        [Log]
        public async Task<AddMovementResponseBe> AddMovement(MovementRequestDto movementRequestDto)
        {
            var movementResponse = new AddMovementResponseBe();
            try
            {
                var result = await _repository.AddMovement(movementRequestDto);
                movementResponse = _mapper.Map<AddMovementResponseBe>(result);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.Message));
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.TypeMap.SourceType));
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.TypeMap.DestinationType));
            }
            return movementResponse;
        }

        [Log]
        public async Task<ChartDataViewModel> GetChartData(long UserId, long AccountId)
        {
            var chartDataViewModel = new ChartDataViewModel();
            try
            {
                var result = await _repository.GetChartData(UserId, AccountId);
                chartDataViewModel = _mapper.Map<ChartDataViewModel>(result);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.Message));
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.TypeMap.SourceType));
                _logger.Error(string.Format(Properties.Resources.AutoMapperMappingException, ex.TypeMap.DestinationType));
            }
            return chartDataViewModel;
        }

        [Log]
        public async Task<List<MovementDetailBe>> GetMovementDetailData(long userId, long accountId)
        {
            var movements = await _repository.GetMovementDetailData(userId, accountId);
            return _mapper.Map<List<MovementDetailBe>>(movements);
        }
    }
}
