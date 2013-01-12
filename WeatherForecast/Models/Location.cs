using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace WeatherForecast.Models
{
    [MetadataType(typeof(Location_Metadata))]
    public partial class Location : EntityObject
    {

    }
    public class Location_Metadata
    {
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public int LocationID { get; set; }
        public DateTime NextUpdate { get; set; }
    }
}
