using MonefyWeb.Application.ModelsWebPage.ViewModels;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public interface IApplicationWebPage
    {
        ChartDataViewModel GetChartData(long UserId, long AccountId);
    }
}