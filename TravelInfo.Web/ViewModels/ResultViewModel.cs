using System;
using System.Collections.Generic;
using TravelInfo.Web.Models;

namespace TravelInfo.Web.ViewModels
{
    public class ResultViewModel
    {
        public Weather Weather { get; set; }
        public Dictionary<string, decimal> Currency { get; set; }
    }
}
