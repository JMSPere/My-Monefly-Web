using MonefyWeb.Application.ModelsWebPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface ICryptoAppService
    {
        Task<List<CryptoDataResponse>> GetCryptoData();
    }
}
