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

        public HomeController(SearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string location, string destination)
        {
            if (!_searchService.IsValidCountry(location) || !_searchService.IsValidCountry(destination))
                return View("NotFound");

            var res = await _searchService.SearchAsync(location, destination);
            return View(res);
        }

        [HttpPost]
        public JsonResult Countries(string prefix)
        {
            var countryNames = _searchService.FilterCountries(prefix);
            return Json(countryNames);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Credits()
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
