using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var keys = names.Keys.ToList();

            keys.ForEach((k) => _countries.Add(
                    new Country(
                        codes.GetValueOrDefault(k),
                        capitals.GetValueOrDefault(k),
                        names.GetValueOrDefault(k),
                        currencyCodes.GetValueOrDefault(k),
                        phoneCodes.GetValueOrDefault(k)
               )));

            //no need to hold dictionaries in memory after creating list.
            //since dictionaries are scopped to method  they are available for garbage collection after method is finished executing.
        }

        public List<Country> GetCountries()
        {
            if (_countries != null)
                return _countries;

            InitializeCountries(); //Very unlikely that this will get called as countries will definitely be initialized at startup
            return _countries;
        }

        public string GetCurrencyCode(string countryName) 
                            =>_countries.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower()).CurrencyCode;

        public string GetCapitalCity(string countryName)
                            => _countries.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower()).Capital;

        public string GetCountryCode(string countryName)
                           => _countries.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower()).Code;


    }
}
