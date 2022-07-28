using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XApplication.Models.Weather;
using XApplication.Services;
using XApplication.Views;

namespace XApplication.ViewModels
{
    public class WeatherMainViewModel: BaseViewModel
    {
        private readonly WeatherDataService _weatherDataService;

        private Location _Location;

        public Location Location
        {
            get { return _Location; }
            set =>SetProperty(ref _Location, value);
        }
        private Current _Today;

        public Current Today
        {
            get { return _Today; }
            set => SetProperty(ref _Today, value);
        }

        public ObservableCollection<Forecastday> ForecastDays { get; set; }
        public ObservableCollection<Hour> ForecastHours { get; set; }

        public AsyncCommand AppearingCommand { get; set; }
        private async Task OnAppearingCommandExecuted()
        {
            try
            {
                IsBusy = true;
                ForecastContainer container = await _weatherDataService.GetForecast(Location, 1);
                Location = container.Location;
                Today = container.Current;
                var forecast_days = container.Forecast.Forecastday;
                ForecastDays.Clear();
                foreach (var day in forecast_days)
                    ForecastDays.Add(day);
                var forecast_hours = ForecastDays.First().Hour;
                ForecastHours.Clear();
                foreach(var hour in forecast_hours)
                    ForecastHours.Add(hour);
            }
            catch(Exception ex) {await  App.Current.MainPage.DisplayAlert("Exception", ex.Message, "Ok"); }
            finally { IsBusy = false; }
        }

        public AsyncCommand NavigateForecastCommand { get; set; }
        private async Task OnNavigateForecastCommandExecuted()
        {
            string json = JsonConvert.SerializeObject(Location);
            await Shell.Current.GoToAsync($"{nameof(ForecastPage)}?Content={json}");
        }

        public AsyncCommand DeleteCurrentLocationCommand { get; set; }
        private async Task OnDeleteCurrentLocationCommandExecuted()
        {
            bool result = await Shell.Current.DisplayAlert("Question", $"Are you sure you want to delete {Location.Name} location?", "Yes", "No");
            if (result)
            {
                MessagingCenter.Send<WeatherMainViewModel, Location>(this, "delete", Location);
            }
        }
        public WeatherMainViewModel(Location location)
        {
            Location = location;
            _weatherDataService = DependencyService.Get<WeatherDataService>();
            ForecastDays = new ObservableCollection<Forecastday>();
            ForecastHours = new ObservableCollection<Hour>();
            AppearingCommand = new AsyncCommand(OnAppearingCommandExecuted);
            NavigateForecastCommand = new AsyncCommand(OnNavigateForecastCommandExecuted);
            DeleteCurrentLocationCommand = new AsyncCommand(OnDeleteCurrentLocationCommandExecuted);
        }
    }
}
