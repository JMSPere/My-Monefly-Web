using MonefyWeb.Infrastructure.DataModels.Response;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Contracts
{
    public interface IRedisCache
    {
        bool AlreadyExistsInCache(string key);
        Task<bool> SetAsyncJson(string key, string value);
        Task<string> GetAsyncJson(string key);
        Task<List<AlphaVantageResponse>> GetAllAsyncJson();
        Task<bool> HasData();
    }
}
