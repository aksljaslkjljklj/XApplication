using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using XApplication.Models.Weather;

namespace XApplication.Services
{
    public class WeatherJsonService
    {
        private readonly string _path = Path.Combine(FileSystem.AppDataDirectory, "saved_locations.json");
        public IEnumerable<Models.Weather.Location> LoadLocations()
        {
            if(!File.Exists(_path))
                return new List<Models.Weather.Location>();
            string json = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<IEnumerable<Models.Weather.Location>>(json);
        }

        public void SaveLocations(IEnumerable<Models.Weather.Location> locations)
        {
            string json = JsonConvert.SerializeObject(locations);
            File.WriteAllText(_path, json);
        }
    }
}
