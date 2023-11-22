using MonefyWeb.Application.ModelsWebPage.ViewModels;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
