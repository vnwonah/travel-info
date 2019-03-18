using System;
using System.Collections.Generic;
using TravelInfo.Web.Models;

namespace TravelInfo.Web.ViewModels
{
    public class ResultViewModel
    {
        public Country Location { get; set; }
        public Country Destination { get; set; }
        public Weather Weather { get; set; }
        public Dictionary<string, decimal> Currency { get; set; }
    }
}
