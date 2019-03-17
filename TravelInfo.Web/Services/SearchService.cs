using System;
using System.Collections.Generic;
using TravelInfo.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace TravelInfo.Web.Services
{
    public class SearchService
    {
        public readonly List<Country> _countries;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SearchService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries.json");

        }

        public async Task<string> ConvertCurrency()
        {
            throw new NotImplementedException();
        }
    }
}
