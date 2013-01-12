using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.ViewModels;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "City")] LocationIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var service = new WeatherService();
                    model.Locations = service.FindLocation(model.City);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Index", model);
        }

        public ActionResult Weather(Location loc)
        {
            try
            {
                var service = new WeatherWebService();
                var model = new WeatherViewModel();
                model.Weathers = service.FindWeather(loc);

                return View("Weather", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Error");
        }
    }
}
