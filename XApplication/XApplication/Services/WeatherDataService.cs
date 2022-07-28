using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using XApplication.Models.Weather;

namespace XApplication.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private readonly string _api = "YOUR_API_KEY";
        private Uri base_url = new Uri("https://api.weatherapi.com/v1/");
        private string GetQuery(string method, Dictionary<string, object> parameters = null)
        {
            string query = $"{method}.json?key={_api}";
            if (parameters != null)
            {
                foreach (var param in parameters)
                    query += $"&{param.Key}={param.Value}";
            }
            return query;
        }
        private async Task<T> GetResponseAsObject<T>(string request)
        {
            using (HttpClient client = new HttpClient { BaseAddress = base_url })
            {
                using (HttpResponseMessage response = await client.GetAsync(request))
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
        public async Task<IEnumerable<Location>> SearchLocation(string parameter)
        {
            string search_request = GetQuery("search", new Dictionary<string, object>
            {
                ["q"] = parameter
            });
            return await GetResponseAsObject<IEnumerable<Location>>(search_request);
        }

        public async Task<Current> GetCurrentWeather(Location location)
        {
            string current_weather_request = GetQuery("current", new Dictionary<string, object>
            {
                ["q"] = location.Name,
                ["aqi"] = "no"
            }) ;
            return await GetResponseAsObject<Current>(current_weather_request);
        }

        public async Task<ForecastContainer> GetForecast(Location location, int days)
        {
            string forecast_request = GetQuery("forecast", new Dictionary<string, object>
            {
                ["q"] = location.Name,
                ["days"] = days,
                ["aqi"] = "no",
                ["alerts"] = "no"
            });
            return await GetResponseAsObject<ForecastContainer>(forecast_request);
        }
    }
}
