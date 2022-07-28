using System.Collections.Generic;
using System.Threading.Tasks;
using XApplication.Models.Weather;

namespace XApplication.Services
{
    public interface IWeatherDataService
    {
        Task<Current> GetCurrentWeather(Location location);
        Task<ForecastContainer> GetForecast(Location location, int days);
        Task<IEnumerable<Location>> SearchLocation(string parameter);
    }
}