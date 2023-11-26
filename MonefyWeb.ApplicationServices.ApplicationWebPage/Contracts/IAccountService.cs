using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.DistributedServices.Models.Models.Movements;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public interface IAccountService
    {
        Task<AddMovementResponseViewModel> AddMovement(MovementRequestDto movementRequestDto);
        Task<ChartDataViewModel> GetChartData(long UserId, long AccountId);
        Task<MovementDetailViewModel> GetMovementDetailData(long userId, long accountId);
    }
}