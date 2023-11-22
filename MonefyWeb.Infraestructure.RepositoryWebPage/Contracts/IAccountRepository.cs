using MonefyWeb.Application.ModelsWebPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public interface IAccountRepository
    {
        ChartDataDto GetChartData(long UserId, long AccountId);
    }
}
