using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherForecast.Models;

namespace WeatherForecast.ViewModels
{
    public class LocationIndexViewModel
    {
        public List<Location> Locations { get; set; }
        public string City { get; set; }
    }
}