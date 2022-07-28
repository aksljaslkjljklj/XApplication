using Newtonsoft.Json;
using System.Collections.Generic;

namespace XApplication.Models.Weather
{
    public class Forecast
    {
        [JsonProperty("forecastday")]
        public List<Forecastday> Forecastday { get; set; }
    }
}
