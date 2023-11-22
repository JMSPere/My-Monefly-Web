using MonefyWeb.Application.ModelsWebPage.ViewModels;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface IAccountDomainWebPage
    {
        ChartDataViewModel GetChartData(long UserId, long AccountId);
    }
}