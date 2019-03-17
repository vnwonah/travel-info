using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelInfo.Web.Helpers;
using TravelInfo.Web.Models;
using TravelInfo.Web.Services;

namespace TravelInfo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchService _searchService;
        private readonly CountryHelper _countryHelper;

        public HomeController(SearchService searchService, CountryHelper countryHelper)
        {
            _searchService = searchService;
            _countryHelper = countryHelper;
        }

        public IActionResult Index()
        {
            _countryHelper.GetCountries();
            return View();
        }

        [HttpGet]
        public IActionResult Search(string location, string destination)
        {
            //Autocomplete
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
