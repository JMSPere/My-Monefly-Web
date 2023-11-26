using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DistributedServices.Models.Models.Movements;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class AccountDomainWebPage : IAccountDomainWebPage
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountDomainWebPage(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

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
                Console.WriteLine($"AutoMapper Mapping Exception: {ex.Message}");
                Console.WriteLine($"Source Type: {ex.TypeMap.SourceType}");
                Console.WriteLine($"Destination Type: {ex.TypeMap.DestinationType}");
            }
            return movementResponse;
        }

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
                Console.WriteLine($"AutoMapper Mapping Exception: {ex.Message}");
                Console.WriteLine($"Source Type: {ex.TypeMap.SourceType}");
                Console.WriteLine($"Destination Type: {ex.TypeMap.DestinationType}");
            }
            return chartDataViewModel;
        }
    }
}
