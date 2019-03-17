using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
