﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeatherWPFProject.WeatherInfo;

namespace WeatherWPFProject
{

    internal class WeatherInfo
    {

        public class Root
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Sys sys { get; set; }

        }
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public double pressure { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
        }

        public class Sys
        {
            public long sunrise { get; set; }
            public long sunset { get; set; }
        }
    }


}
