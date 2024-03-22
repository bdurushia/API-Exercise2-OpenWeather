using Newtonsoft.Json.Linq;

var client = new HttpClient();

var appsettingsText = File.ReadAllText("appsettings.json");

var apiKey = JObject.Parse(appsettingsText)["apiKey"].ToString();
// var zipApiKey = JObject.Parse(appsettingsText)["zipApiKey"].ToString();

int zipCode = 0;
bool isZip = false;

// Use a Try Catch if not a valid zip code
while (!isZip)
{
    Console.Clear();
    Console.WriteLine("\n\t\tWelcome to The Open Weather App!!!\n");
    Console.Write("\nEnter the zipcode of the city area you want to check the weather forecast for: ");
    isZip = int.TryParse(Console.ReadLine(), out zipCode);

    if (isZip == false)
    {
        Console.WriteLine("Invalid zip code entry, please try again.\n(Press enter to continue).");
        Console.ReadLine();
        continue;
    }
    else
    {
        isZip = true;
    }
}

Console.Clear();

// 

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

Console.WriteLine($"\n\tCity: {cityName}, {state}\n\tTemp: {temp}\u00B0F\n\tReal-Feel: {feelsLike}\u00B0F");