using Newtonsoft.Json;
using System;

namespace XApplication.Models.Weather
{
    public class Condition
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        private string _Icon;

        [JsonProperty("icon")]
        public string Icon
        {
            get 
            {
                if(_Icon.StartsWith("code_"))
                    return _Icon;
                return "code_"+_Icon.Substring(_Icon.Length - 7, 6);
            }
            set { _Icon = value; }
        }


        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
