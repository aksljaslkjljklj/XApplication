using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XApplication.Models.Weather;
using XApplication.Services;

namespace XApplication.ViewModels
{
    public class WeatherTabbedViewModel: MvvmHelpers.BaseViewModel
    {
        private readonly WeatherJsonService _weatherJsonService;
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<BaseViewModel> ViewModels { get; set; }
        private void LoadViewModels()
        {
            Locations.Clear();
            foreach(var location in _weatherJsonService.LoadLocations())
                Locations.Add(location);

            ViewModels.Clear();
            foreach (var location in Locations)
                ViewModels.Add(new WeatherMainViewModel(location));
            ViewModels.Add(new NewLocationViewModel());
        }
        public WeatherTabbedViewModel()
        {
            _weatherJsonService = DependencyService.Get<WeatherJsonService>();
            Locations = new ObservableCollection<Location>();
            ViewModels = new ObservableCollection<BaseViewModel>();
            LoadViewModels();
            MessagingCenter.Subscribe<NewLocationViewModel, Location>(this, "add", (sender, arg) =>
            {
                Locations.Add(arg);
                ViewModels.Insert(ViewModels.Count-1, new WeatherMainViewModel(arg));
                _weatherJsonService.SaveLocations(Locations.ToList());
            });
            MessagingCenter.Subscribe<WeatherMainViewModel, Location>(this, "delete", (sender, arg) =>
            {
                var location = Locations.FirstOrDefault(l => l.Name == arg.Name);
                Locations.Remove(location);
                _weatherJsonService.SaveLocations(Locations.ToList());
                LoadViewModels();
            });
        }
    }
}
