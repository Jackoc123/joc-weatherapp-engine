using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Application.Weather.Models
{
    public class WeatherResponse
    {
        public string name { get; set; }
        public WeatherMain main { get; set; }
        public WeatherSys sys { get; set; }
        
    }

    public class WeatherMain
    {
        public float temp { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public float pressure { get; set; }
        public float humidity { get; set; }

    }

    public class WeatherSys
    {
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
}
