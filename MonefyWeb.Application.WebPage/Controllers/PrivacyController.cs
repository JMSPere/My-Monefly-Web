using Microsoft.AspNetCore.Mvc;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly string apiVersion = "v2";

        public ActionResult Privacy()
        {
            return View();
        }
    }
}
