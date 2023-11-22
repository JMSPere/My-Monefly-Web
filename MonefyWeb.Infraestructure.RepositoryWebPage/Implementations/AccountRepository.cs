using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infraestructure.ServiceAgentsWebPage;

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
