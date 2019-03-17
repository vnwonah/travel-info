using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using TravelInfo.Web.Models;


namespace TravelInfo.Web.Helpers
{
    public class CountryHelper
    {
        private List<Country> _countries;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CountryHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            InitializeCountries();
        }

        void InitializeCountries()
        {
            //Couldn't find an API to get comprehensive country data 
            //so I am constructing the countries list from json files


            //because this class is registered in the DI as a singleton this operation runs just once, on startup
            //minimizing resource usage.

            //Rationale behind maintaining this locally rather than pulling from an API is that countries don't
            //get created too often so change is minimal

            _countries = new List<Country>();

            var capitals = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                            File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries/Capitals.json")));

            var codes = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                            File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries/Codes.json")));

            var names = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                            File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries/CountryNames.json")));

            var currencyCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries/CurrencyCodes.json")));

            var phoneCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                                File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "Data/Countries/PhoneCodes.json")));

            foreach (var name in names)
            {
                var countryName = name.Value;
                var countryCode = codes.GetValueOrDefault(name.Key);
                var countryCapital = capitals.GetValueOrDefault(name.Key);
                var currencyCode = currencyCodes.GetValueOrDefault(name.Key);
                var phoneCode = phoneCodes.GetValueOrDefault(name.Key);

                var country = new Country(countryCode, countryCapital, countryName, currencyCode, phoneCode);
                _countries.Add(country);
                
            } 
        }

        public List<Country> GetCountries()
        {
            if (_countries != null)
                return _countries;

            InitializeCountries();
            return _countries;
        }

    }
}
