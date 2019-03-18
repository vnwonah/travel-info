using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TravelInfo.Web.Helpers;
using TravelInfo.Web.Models;
using TravelInfo.Web.ViewModels;

namespace TravelInfo.Web.Services
{
    public class SearchService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _currencyApiToken;
        private readonly string _weatherAPIKey;
        private readonly CountryHelper _countryHelper;



        public SearchService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            CountryHelper countryHelper)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _currencyApiToken = _configuration["CurrencyAPIToken"];
            _weatherAPIKey = _configuration["OpenWeatherMapKey"];
            _countryHelper = countryHelper;
            
        }

        internal async Task<Weather> GetWeatherInfoAsync(string destinationCity)
        {
            var client = _clientFactory.CreateClient("weatherapi");
            try
            {
                var queryString = String.Format("?q={0}&units=metric&APPID={1}", destinationCity, _weatherAPIKey);
                var res = await client.GetAsync(queryString);

                if (!res.IsSuccessStatusCode)
                    return null;

                return JsonConvert.DeserializeObject<Weather>(await res.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                client.Dispose();
            }
        }

        internal async Task<Dictionary<string, decimal>> ConvertCurrencyAsync(string fromCurrency, string toCurrency)
        {
            var client = _clientFactory.CreateClient("currencyapi");

            try
            {
                var queryString = String.Format("?q={0}_{1}&compact=ultra&apiKey={2}", fromCurrency, toCurrency, _currencyApiToken);
                var res = await client.GetAsync(queryString);

                if (!res.IsSuccessStatusCode)
                    return null;

                return JsonConvert.DeserializeObject<Dictionary<string, decimal>>(await res.Content.ReadAsStringAsync());

            }
            catch (Exception)
            {
                //log exception
                return null;
            }
            finally
            {
                client.Dispose();
            }

        }

        internal object FilterCountries(string keyword)
        {
            var countries = from c in _countryHelper.GetCountries()
                            where c.Name.ToLower().StartsWith(keyword.ToLower(),
                                  StringComparison.CurrentCulture)
                            select new { c.Name };

            return countries;
        }

        internal async Task<ResultViewModel> SearchAsync(string location, string destination)
        {
            var result = new ResultViewModel
            {
                Destination = _countryHelper.GetCountry(destination),
                Location = _countryHelper.GetCountry(location),
                Currency = await ConvertCurrencyAsync(_countryHelper.GetCurrencyCode(location), _countryHelper.GetCurrencyCode(destination)),
                Weather = await GetWeatherInfoAsync(_countryHelper.GetCapitalCity(destination))
            };
            return result;
        }
    }
}
