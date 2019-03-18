﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TravelInfo.Web.Helpers;
using TravelInfo.Web.ViewModels;

namespace TravelInfo.Web.Services
{
    public class SearchService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _currencyApiToken;
        private readonly CountryHelper _countryHelper;

        public SearchService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration,
            CountryHelper countryHelper)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _currencyApiToken = _configuration["CurrencyAPIToken"];
            _countryHelper = countryHelper;
            
        }

        public async Task<Dictionary<string, decimal>> ConvertCurrencyAsync(string fromCurrency, string toCurrency)
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
            catch (Exception ex)
            {
                //log exception
                return null;
            }
            finally
            {
                client.Dispose();
            }

        }

        internal async Task<ResultViewModel> SearchAsync(string location, string destination)
        {
            var result = new ResultViewModel
            {
                Currency = await ConvertCurrencyAsync(_countryHelper.GetCurrencyCode(location), _countryHelper.GetCurrencyCode(destination))
            };
            return result;
        }
    }
}
