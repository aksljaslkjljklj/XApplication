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
    public class NewLocationViewModel: BaseViewModel
    {
        private readonly WeatherDataService _weatherDataService;

        private Location _SelectedLocation;

        public Location SelectedLocation
        {
            get { return _SelectedLocation; }
            set
            {
                SetProperty(ref _SelectedLocation, value);
                AddNewLocationCommand.RaiseCanExecuteChanged();
            }
        }

        private string _SearchRequest;

        public string SearchRequest
        {
            get { return _SearchRequest; }
            set 
            { 
                SetProperty(ref _SearchRequest, value);
            }
        }

        public ObservableCollection<Grouping<string,Location>> SearchResults { get; set; }
        public AsyncCommand<string> SearchCommand { get; set; }
        private async Task OnSearchCommandExecuted(string parameter)
        {
            IsBusy = true;
            try
            {
                var list = await _weatherDataService.SearchLocation(SearchRequest);
                IEnumerable<Grouping<string, Location>> groups = list.GroupBy(l => l.Region)
                                                                     .Select(g => new Grouping<string, Location>(g.Key, g.Select(l => l)))
                                                                     .ToList();
                SearchResults.Clear();
                if (groups.Count() > 0)
                {
                    foreach (Grouping<string, Location> group in groups)
                        SearchResults.Add(group);
                }
            }
            catch (Exception ex) { await App.Current.MainPage.DisplayAlert("ex", ex.Message, "Ok"); }
            finally { IsBusy = false; }

        }

        public AsyncCommand AddNewLocationCommand { get; set; }
        private async Task OnAddNewLocationCommandExecuted()
        {
            MessagingCenter.Send(this, "add", SelectedLocation);
            SearchResults.Clear();
            SearchRequest = string.Empty;
            SelectedLocation = null;
            await Task.FromResult(true);
        }
        private bool CanAddNewLocationCommandExecuted(object p) => SelectedLocation != null;

        public AsyncCommand<Location> ShowLocationDetailsCommand { get; set; }
        private async Task OnShowLocationDetailsCommandExecuted(Location parameter)
        {
            string json = JsonConvert.SerializeObject(parameter);
            await Shell.Current.GoToAsync($"{nameof(LocationDetailsPage)}?Content={json}");
        }
        public NewLocationViewModel()
        {
            _weatherDataService = DependencyService.Get<WeatherDataService>();
            SearchResults = new ObservableCollection<Grouping<string, Location>>();
            SearchCommand = new AsyncCommand<string>(OnSearchCommandExecuted);
            ShowLocationDetailsCommand = new AsyncCommand<Location>(OnShowLocationDetailsCommandExecuted);
            AddNewLocationCommand = new AsyncCommand(OnAddNewLocationCommandExecuted, CanAddNewLocationCommandExecuted);
            SearchRequest = string.Empty;
        }
    }
}
