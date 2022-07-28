using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XApplication.Models.Weather;

namespace XApplication.ViewModels
{
    [QueryProperty(nameof(Content),nameof(Content))]
    public class LocationDetailsViewModel:BaseViewModel
    {
        private string _Content;
        public string Content
        {
            get { return _Content; }
            set
            {
                SetProperty(ref _Content, value);
                SerializeContent(value);
            }
        }
        private Location _Location;

        public Location Location
        {
            get { return _Location; }
            set =>SetProperty(ref _Location, value);
        }

        public AsyncCommand BackCommand { get; set; }
        private async Task OnBackCommandExecuted()
        {
            await Shell.Current.GoToAsync("..");
        }

        private void SerializeContent(string json)
        {
            Location = JsonConvert.DeserializeObject<Location>(json);
        }
        public LocationDetailsViewModel()
        {
            BackCommand = new AsyncCommand(OnBackCommandExecuted);
        }
    }
}
