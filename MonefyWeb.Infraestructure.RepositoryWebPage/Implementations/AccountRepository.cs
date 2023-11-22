using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infraestructure.ServiceAgentsWebPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IApiServiceAgent _service;
        public ChartDataDto GetChartData(long UserId, long AccountId)
        {
            throw new NotImplementedException();
        }
    }
}
