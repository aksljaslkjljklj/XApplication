using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XApplication.Models.Weather;
using XApplication.Services;

namespace TestConsole
{
    public class Program
    {
        private static WeatherDataService _weatherDataService;
        static void Main(string[] args)
        {
            _weatherDataService = new WeatherDataService();
            MainAsync().Wait();
            Console.ReadKey();
        }
        static async Task MainAsync()
        {
            IEnumerable<Location> forecast = await _weatherDataService.SearchLocation("London");
            string json = JsonConvert.SerializeObject(forecast);
            File.WriteAllText("forecast.json", json);
        }
    }
}
