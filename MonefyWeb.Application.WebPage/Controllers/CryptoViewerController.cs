using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;

public class CryptoViewerController : Controller
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICryptoAppService _cryptoAppService;

    private readonly string apiVersion = "v2";
    private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
    // private readonly string baseUrl = "https://localhost:7006/api/";

    public CryptoViewerController(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, ICryptoAppService cryptoAppService)
    {
        _memoryCache = memoryCache;
        _httpClientFactory = httpClientFactory;
        _cryptoAppService = cryptoAppService;
    }

    public async Task<ActionResult> Index()
    {
        if (_memoryCache.TryGetValue<long>("UserId", out var userId) &&
                _memoryCache.TryGetValue<long>("AccountId", out var accountId))
        {
            var cryptoDataList = await GenerateCryptoDataList();
            if (cryptoDataList is null || cryptoDataList.Count == 0)
                return View("Error");
            ViewBag.Crypto = cryptoDataList;
            return View();
        } else {
            return RedirectToAction("Login", "Login");
        }
            
    }

    public async Task<List<CryptoDataResponse>> GenerateCryptoDataList()
    {
        var cryptoDataList = await _cryptoAppService.GetCryptoData();
        return cryptoDataList;
    }
}
