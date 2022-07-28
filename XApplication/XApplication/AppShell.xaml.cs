using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XApplication.Views;

namespace XApplication
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ForecastPage),typeof(ForecastPage));
            Routing.RegisterRoute(nameof(NewLocationPage), typeof(NewLocationPage));
            Routing.RegisterRoute(nameof(LocationDetailsPage), typeof(LocationDetailsPage));
        }

    }
}
