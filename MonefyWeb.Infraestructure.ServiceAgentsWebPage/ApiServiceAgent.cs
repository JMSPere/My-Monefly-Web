namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ApiServiceAgent
    {
        private readonly HttpClient httpClient;

        public ApiServiceAgent()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> RealizarSolicitudGetAsync(string apiUrl, Dictionary<string, string> parametros)
        {
            try
            {
                string queryString = string.Join("&", parametros.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
                string urlCompleta = $"{apiUrl}?{queryString}";

                HttpResponseMessage response = await httpClient.GetAsync(urlCompleta);

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

        public async Task<string> RealizarSolicitudPostAsync(string apiUrl, object data)
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
