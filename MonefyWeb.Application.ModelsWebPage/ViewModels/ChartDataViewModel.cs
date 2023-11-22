using MonefyWeb.DistributedServices.Models.Models.Accounts;
using MonefyWeb.DistributedServices.Models.Models.Categories;

namespace MonefyWeb.Application.ModelsWebPage.ViewModels
{
    public class ChartDataViewModel
    {
        public long UserId { get; set; }
        public long AccountIds { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<AccountMovementDto> AccountMovementDtos { get; set; }
    }
}