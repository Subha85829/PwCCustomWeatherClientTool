using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomWeatherClientTool;
using CustomWeatherClientTool.Models;

namespace CustomWeatherClientTool
{
    public class WeatherDetails
    {
        public static async Task getDetails(string enteredCityName)
        {
            string cityName = enteredCityName;
            // Getting the city data from json file

            string getCityDetails = await File.ReadAllTextAsync(@".\Cities.json");
            CityDetails[] cities = JsonSerializer.Deserialize<CityDetails[]>(getCityDetails);

            // Searching the city from the array

            CityDetails? city = cities.FirstOrDefault(x => string.Equals(x.city, enteredCityName, StringComparison.OrdinalIgnoreCase));

            // Checking if the city is present or not

            if (city == null)
            {
                Console.WriteLine($"Sorry, {enteredCityName} not found!");
                Console.ReadLine();
                return;
            }
            // To get the weather details provided in the document

            string url = $"https://api.open-meteo.com/v1/forecast?latitude={city.lat}&longitude={city.lng}&current_weather=true";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Checking the Base URL response 

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JsonDocument jsonDocument = JsonDocument.Parse(json);
                    JsonElement jsonElement = jsonDocument.RootElement;

                    // Storing the value in a variable

                    double Temp = jsonElement.GetProperty("current_weather").GetProperty("temperature").GetDouble();
                    double windSpeed = jsonElement.GetProperty("current_weather").GetProperty("windspeed").GetDouble();
                    double windDirection = jsonElement.GetProperty("current_weather").GetProperty("winddirection").GetDouble();
                    double weatherCode = jsonElement.GetProperty("current_weather").GetProperty("weathercode").GetDouble();
                    DateTime Time = jsonElement.GetProperty("current_weather").GetProperty("time").GetDateTime();


                    // Printing the weather details 

                    Console.WriteLine();
                    Console.WriteLine($"Your city name is {enteredCityName}");
                    Console.WriteLine($"Current temparature is : {Temp}");
                    Console.WriteLine($"Current wind speed is : {windSpeed}");
                    Console.WriteLine($"Current wind direction is : {windDirection}");
                    Console.WriteLine($"Current weather code is : {weatherCode}");
                    Console.WriteLine($"Last updated at : {Time}");
                    Console.ReadLine();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error message : " + e.Message);
                }
            }
        }

    }
}
