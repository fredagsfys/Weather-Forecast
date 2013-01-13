using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace WeatherForecast.Models
{
    [MetadataType(typeof(Weather_Metadata))]
    public partial class Weather : EntityObject
    {
        public DateTime NextUpdate { get; set; }
    }
    public class Weather_Metadata
    {
        public string Time { get; set; }
        public string Symbol { get; set; }
        public int LocationID { get; set; }
        public string Temp { get; set; }
      
    }
}