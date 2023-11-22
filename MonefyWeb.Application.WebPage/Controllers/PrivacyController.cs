using Microsoft.AspNetCore.Mvc;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public ActionResult Privacy()
        {
            return View();
        }
    }
}
