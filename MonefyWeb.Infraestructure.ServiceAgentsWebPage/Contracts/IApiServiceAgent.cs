namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage
{
    public interface IApiServiceAgent
    {
        Task<string> GetApiAsync(string apiUrl);

        Task<string> PostApiAsync(string apiUrl, string contenido);
    }
}
