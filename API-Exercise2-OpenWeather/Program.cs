using Newtonsoft.Json.Linq;

var client = new HttpClient();

var appsettingsText = File.ReadAllText("appsettings.json");

var apiKey = JObject.Parse(appsettingsText)["apiKey"].ToString();

int zipCode = 0;
bool isZip = false;

// Use a Try Catch if not a valid zip code
while (!isZip)
{
    Console.Clear();
    Console.WriteLine("Enter the zipcode of the city area you want to check the weather forecast for: ");
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

var openWeatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&appid={apiKey}&units=imperial";

var weatherResponse = client.GetStringAsync(openWeatherURL).Result;
var cityName = JObject.Parse(weatherResponse)["name"].ToString();
var temp = JObject.Parse(weatherResponse)["main"]["temp"].ToString();
var feelsLike = JObject.Parse(weatherResponse)["main"]["feels_like"].ToString();

Console.WriteLine($"City: {cityName}\nTemp: {temp}\u00B0F\nReal-Feel: {feelsLike}\u00B0F");