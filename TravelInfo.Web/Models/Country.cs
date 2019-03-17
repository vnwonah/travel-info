using System;
using System.Collections.Generic;

namespace TravelInfo.Web.Models
{
    public class Country
    {
        public Country(string code, string capital, string name, string currency, string phone)
        {
            Code = code;
            Capital = capital;
            Name = name;
            CurrencyCode = currency;
            PhoneCode = phone;

        }
        public string Code { get; }
        public string Capital { get; }
        public string Name { get; }
        public string CurrencyCode { get; }
        public string PhoneCode { get; }
    }
}
