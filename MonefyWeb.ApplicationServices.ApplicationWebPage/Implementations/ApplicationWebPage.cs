using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.Application.WebPage.Controllers;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class ApplicationWebPage : IApplicationWebPage
    {
        private IAccountDomainWebPage _domain;

        public ApplicationWebPage(IAccountDomainWebPage _domain)
        {
            this._domain = _domain;
        }
        public ChartDataViewModel GetChartData(long UserId, long AccountId)
        {
            return _domain.GetChartData(UserId, AccountId);
        }
    }
}
