using MonefyWeb.Application.ModelsWebPage.ViewModels;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public interface IApplicationWebPage
    {
        ChartDataViewModel GetChartData(long userId, long accountId);
    }
}