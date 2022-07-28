using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XApplication.ViewModels;

namespace XApplication.Other
{
    public class WeatherTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WeatherTemplate { get; set; }
        public DataTemplate NewLocationTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is WeatherMainViewModel)
                return WeatherTemplate;
            else
                return NewLocationTemplate;
        }
    }
}
