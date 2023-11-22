using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;

public class CryptoViewerController : Controller
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICryptoAppService _cryptoAppService;

    public CryptoViewerController(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, ICryptoAppService cryptoAppService)
    {
        _memoryCache = memoryCache;
        _httpClientFactory = httpClientFactory;
        _cryptoAppService = cryptoAppService;
    }

    public async Task<ActionResult> Index()
    {
        List<CryptoDataResponse> cryptoDataList = await GenerateCryptoDataList();
        if (cryptoDataList is null || cryptoDataList.Count == 0)
            return View("Error");
        ViewBag.Crypto = cryptoDataList;
        return View();
    }

    public async Task<List<CryptoDataResponse>> GenerateCryptoDataList()
    {
        List<CryptoDataResponse> cryptoDataList = new List<CryptoDataResponse>();
        cryptoDataList = await _cryptoAppService.GetCryptoData();
        return cryptoDataList;
    }
}
