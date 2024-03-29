﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonefyWeb.Application.WebPage.Models;

namespace MonefyWeb.Application.WebPage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memory;

        private readonly string apiVersion = "v2";
        private readonly string baseUrl = "https://moneflyapi.azurewebsites.net/api/";
        // private readonly string baseUrl = "https://localhost:7006/api/";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IMemoryCache _memory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            this._memory = _memory;
        }

        public IActionResult Index()
        {
            _memory.Set("UserId", 1L);
            _memory.Set("AccountId", 1L);
            _memory.Set("SessionToken", "tokensillu");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}