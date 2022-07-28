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

namespace XApplication.ViewModels
{
    [QueryProperty(nameof(Content),nameof(Content))]
    public class ForecastViewModel: MvvmHelpers.BaseViewModel
    {
        private readonly WeatherDataService _weatherDataService;
        public ObservableCollection<Forecastday> ForecastItems { get; set; }

        private Forecastday _SelectedDay;
        public Forecastday SelectedDay
        {
            get { return _SelectedDay; }
            set => SetProperty(ref _SelectedDay, value);
        }

        private string _Content;

        public string Content
        {
            get { return _Content; }
            set 
            { 
                _Content = value;
                Location = JsonConvert.DeserializeObject<Location>(value);
            }
        }

        private Location Location { get; set; }

        public AsyncCommand AppearingCommand { get; set; }
        private async Task OnAppearingCommandExecuted()
        {
            IsBusy = true;
            try
            {
                IsBusy = true;
                var forecast = await _weatherDataService.GetForecast(Location, 3);
                ForecastItems.Clear();
                foreach (var day in forecast.Forecast.Forecastday)
                {
                    ForecastItems.Add(day);
                }
                SelectedDay = ForecastItems.FirstOrDefault();
            }
            catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "Ok"); }
            finally { IsBusy = false; }
        }
        public AsyncCommand BackCommand { get; set; }
        private async Task OnBackCommandExecuted()
        {
            await Shell.Current.GoToAsync("..");
        }
        public ForecastViewModel()
        {
            _weatherDataService = DependencyService.Get<WeatherDataService>();
            ForecastItems = new ObservableCollection<Forecastday>();
            AppearingCommand = new AsyncCommand(OnAppearingCommandExecuted);
            BackCommand = new AsyncCommand(OnBackCommandExecuted);
        }
    }
}
