using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Exercise2_OpenWeather
{
    public class Weather
    {
        public static void GetWeather(int zipCode, bool isZip)
        {
            var client = new HttpClient();

            var appsettingsText = File.ReadAllText("appsettings.json");

            var apiKey = JObject.Parse(appsettingsText)["apiKey"].ToString();
            // var zipApiKey = JObject.Parse(appsettingsText)["zipApiKey"].ToString();

            var openWeatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&appid={apiKey}&units=imperial";
            // var zipWeatherURL = $"https://www.zipcodeapi.com/rest/{zipApiKey}/info.json/{zipCode}/degrees";

            var weatherResponse = client.GetStringAsync(openWeatherURL).Result;
            // var zipWeatherResponse = client.GetStringAsync(zipWeatherURL).Result;

            var latitude = JObject.Parse(weatherResponse)["coord"]["lat"].ToString();
            var longitude = JObject.Parse(weatherResponse)["coord"]["lon"].ToString();

            var latLongUrl = $"http://api.openweathermap.org/geo/1.0/reverse?lat={latitude}&lon={longitude}&appid={apiKey}";

            var coordinateResponse = client.GetStringAsync(latLongUrl).Result;

            var cityName = JObject.Parse(weatherResponse)["name"].ToString();
            var state = JArray.Parse(coordinateResponse)[0]["state"].ToString();
            var temp = JObject.Parse(weatherResponse)["main"]["temp"].ToString();
            var feelsLike = JObject.Parse(weatherResponse)["main"]["feels_like"].ToString();
            var tempMin = JObject.Parse(weatherResponse)["main"]["temp_min"].ToString();
            var tempMax = JObject.Parse(weatherResponse)["main"]["temp_max"].ToString();
            var description = JObject.Parse(weatherResponse)["weather"][0]["description"].ToString();

            Connecting.PrintConnection();

            Console.WriteLine($"\n\tCurrent weather for:\n\t{cityName}, {state}");
            Console.WriteLine($"\n\t{description}\n\tTemp: {temp}\u00B0F\n\tReal-Feel: {feelsLike}\u00B0F" +
                              $"\n\tHi: {tempMax}\u00B0F\n\tLo: {tempMin}\u00B0F");
        }
    }
}
