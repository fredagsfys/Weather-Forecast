using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherForecast.Models.Interfaces;
using WeatherForecast.Models.AbstractClasses;

namespace WeatherForecast.Models
{
    public class WeatherService : IWeatherService
    {
        private IWeatherRepository _repository;

        public WeatherService()
            : this(new WeatherRepository())
        {
            // Blank!
        }

        public WeatherService(IWeatherRepository repository)
        {
            this._repository = repository;
        }

        public List<Location> FindLocation(string city)
        {
            // Try to get the user from the database.
                var currentLocation = this._repository
                .QueryLocation()
                .Where(u => u.City.Contains(city))
                .ToList();

            // If there is no user...
            if (currentLocation == null || currentLocation.Count == 0)
            {
                //// ...get the user from the web service, and...
                var webService = new WeatherWebService();
                currentLocation = webService.FindLocation(city);

                // ...save the user in the database.
                foreach (var item in currentLocation)
                {
                    this._repository.Add(item);
                }
                
                this._repository.Save();
            }

            return currentLocation;
        }
        public List<Weather> FindWeather(Location location)
        {
            var weather = this._repository
            .QueryWeather()
            .SingleOrDefault(u => u.LocationID == location.LocationID);
            

            // If there are no tweets or if it is time to uppdate the tweets...
            if (!location.Weathers.Any() || location.NextUpdate < DateTime.Now)
            {
                // ...delete the old(?) tweets (if there are any),...
                location.Weathers
                    .ToList()
                    .ForEach(t => this._repository.Delete(t));
                   
                // ...get the tweets from the web service, and add them to the user,...
                var webService = new WeatherWebService();
                webService.FindWeather(location)
                    .ForEach(t => location.Weathers.Add(t));

                // ...set the time of the next update and ...
                location.NextUpdate = DateTime.Now.AddMinutes(1);
                this._repository.Update(weather);

                // ...save the changes in the database.
                this._repository.Save();
            }

            return location.Weathers.ToList();
        }
    }
}