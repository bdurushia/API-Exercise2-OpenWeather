using API_Exercise2_OpenWeather;
using Newtonsoft.Json.Linq;

var client = new HttpClient();

var appsettingsText = File.ReadAllText("appsettings.json");

var apiKey = JObject.Parse(appsettingsText)["apiKey"].ToString();
// var zipApiKey = JObject.Parse(appsettingsText)["zipApiKey"].ToString();

int zipCode = 0;
bool isZip = false;

var openWeatherURL = "";
var weatherResponse = "";

var latitude = "";
var longitude = "";
var latLongUrl = "";

var coordinateResponse = "";

var cityName = "";
var state = "";
var temp = "";
var feelsLike = "";
var tempMin = "";
var tempMax = "";
var description = "";


// Use a Try Catch if not a valid zip code
while (!isZip)
{
    Console.Clear();

    try
    {
        Welcome.Message();
        Console.Write("\n\tTo check the current weather forecast,\n\tEnter the zipcode for that area: ");
        isZip = int.TryParse(Console.ReadLine(), out zipCode);

        openWeatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&appid={apiKey}&units=imperial";
        // var zipWeatherURL = $"https://www.zipcodeapi.com/rest/{zipApiKey}/info.json/{zipCode}/degrees";

        weatherResponse = client.GetStringAsync(openWeatherURL).Result;
        // var zipWeatherResponse = client.GetStringAsync(zipWeatherURL).Result;

        latitude = JObject.Parse(weatherResponse)["coord"]["lat"].ToString();
        longitude = JObject.Parse(weatherResponse)["coord"]["lon"].ToString();

        latLongUrl = $"http://api.openweathermap.org/geo/1.0/reverse?lat={latitude}&lon={longitude}&appid={apiKey}";

        coordinateResponse = client.GetStringAsync(latLongUrl).Result;

        cityName = JObject.Parse(weatherResponse)["name"].ToString();
        state = JArray.Parse(coordinateResponse)[0]["state"].ToString();
        temp = JObject.Parse(weatherResponse)["main"]["temp"].ToString();
        feelsLike = JObject.Parse(weatherResponse)["main"]["feels_like"].ToString();
        tempMin = JObject.Parse(weatherResponse)["main"]["temp_min"].ToString();
        tempMax = JObject.Parse(weatherResponse)["main"]["temp_max"].ToString();
        description = JObject.Parse(weatherResponse)["weather"][0]["description"].ToString();


        Connecting.PrintConnection();

        Console.WriteLine($"\n\tCurrent weather for:\n\t{cityName}, {state}");
        Console.WriteLine($"\n\t{description}\n\tTemp: {temp}\u00B0F\n\tReal-Feel: {feelsLike}\u00B0F" +
                          $"\n\tHi: {tempMax}\u00B0F\n\tLo: {tempMin}\u00B0F");

        Console.Write("\n\tPress 'Enter' to search again or 'ESC' to quit: ");
        ConsoleKeyInfo keyStroke = Console.ReadKey();
        if (keyStroke.Key == ConsoleKey.Enter)
        {
            isZip = false;
            continue;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("\n\tThank you for using the Open Weather App!!!\n\n\tGoodbye!!!\n\n");
        }
    }
    catch (Exception err) 
    {
        isZip = false;
        Console.WriteLine($"\n\tFailed to retrieve data.\n\t{err.Message} ");
        Console.Write("\n\tInvalid zip code entry, please try again.\n\t(Press enter to continue): ");
        Console.ReadLine();
        continue;
    }
}
