using Newtonsoft.Json;
using System;
using System.Text;

namespace XApplication.Models.Weather
{

    public class ForecastContainer
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("forecast")]
        public Forecast Forecast { get; set; }
    }
}
