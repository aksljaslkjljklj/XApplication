using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XApplication.Services;
using XApplication.Views;

namespace XApplication
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<WeatherDataService>();
            DependencyService.Register<WeatherJsonService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
