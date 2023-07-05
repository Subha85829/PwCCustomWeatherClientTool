using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWeatherClientTool.Models
{
    internal class CityDetails
    {
        public string? city { get; set; } = string.Empty;
        public string? lat { get; set; } = string.Empty;
        public string? lng { get; set; } = string.Empty;
    }
}
