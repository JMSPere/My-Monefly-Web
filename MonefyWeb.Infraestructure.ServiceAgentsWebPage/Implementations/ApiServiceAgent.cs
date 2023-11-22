using System.Text;
using System.Text.Json;
using MonefyWeb.DomainServices.RepositoryContracts.Contracts;

namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage.Implementations
{
    public class ApiServiceAgent : IApiServiceAgent
    {
        private readonly HttpClient httpClient;

        public ApiServiceAgent()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetApiAsync(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error en la solicitud GET: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud GET: {ex.Message}");
            }
            return null;
        }

        public async Task<string> PostApiAsync(string apiUrl, object data)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = false
                };

                string json = JsonSerializer.Serialize(data, options);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string respuesta = await response.Content.ReadAsStringAsync();
                    return respuesta;
                }
                else
                {
                    Console.WriteLine($"Error en la solicitud POST: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud POST: {ex.Message}");
            }
            return null;
        }
    }
}
