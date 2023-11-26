namespace MonefyWeb.DomainServices.RepositoryContracts.Contracts
{
    public interface IApiServiceAgent
    {
        Task<string> GetApiAsync(string apiUrl);
        Task<string> PostApiAsync(string apiUrl, object data);
    }
}