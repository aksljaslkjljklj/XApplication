using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherTabbedMainPage : TabbedPage
    {
        public WeatherTabbedMainPage()
        {
            InitializeComponent();
        }
    }
}