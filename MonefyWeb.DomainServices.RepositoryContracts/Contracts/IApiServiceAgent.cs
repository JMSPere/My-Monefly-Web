namespace MonefyWeb.DomainServices.RepositoryContracts.Contracts
{
    public interface IApiServiceAgent
    {
        Task<string> GetApiAsync(string apiUrl, string token);
        Task<string> PostApiAsync(string apiUrl, string token, object data);
    }
}