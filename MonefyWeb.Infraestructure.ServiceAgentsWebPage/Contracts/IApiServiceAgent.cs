using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.Infraestructure.ServiceAgentsWebPage
{
    public interface IApiServiceAgent
    {
        Task<string> GetApiAsync(string apiUrl);

        Task<string> PostApiAsync(string apiUrl, string contenido);
    }
}
