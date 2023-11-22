using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.DistributedServices.Models.Models.Movements;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface IAccountDomainWebPage
    {
        Task<AddMovementResponseBe> AddMovement(MovementRequestDto movementRequestDto);
        Task<ChartDataViewModel> GetChartData(long UserId, long AccountId);
    }
}