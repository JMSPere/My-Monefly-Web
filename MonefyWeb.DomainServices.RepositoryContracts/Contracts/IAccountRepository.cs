using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using MonefyWeb.DistributedServices.Models.Models.Movements;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public interface IAccountRepository
    {
        Task<AddMovementResponseDto> AddMovement(MovementRequestDto movementRequestDto);
        Task<ChartDataDto> GetChartData(long UserId, long AccountId);
        Task<List<MovementDetailDto>> GetMovementDetailData(long userId, long accountId);
    }
}
