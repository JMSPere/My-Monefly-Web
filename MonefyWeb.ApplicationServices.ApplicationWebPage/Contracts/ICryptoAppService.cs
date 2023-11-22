using MonefyWeb.Application.ModelsWebPage.Models;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts
{
    public interface ICryptoAppService
    {
        Task<List<CryptoDataResponse>> GetCryptoData();
    }
}
