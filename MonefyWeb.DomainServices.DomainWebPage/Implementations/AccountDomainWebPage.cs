using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;

namespace MonefyWeb.DomainServices.DomainWebPage.Implementations
{
    public class AccountDomainWebPage : IAccountDomainWebPage
    {
        private readonly IApiServiceAgent _service;

        ChartDataViewModel IAccountDomainWebPage.GetChartData(long UserId, long AccountId)
        {
            throw new NotImplementedException();
        }
    }
}
