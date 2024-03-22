using API_Exercise2_OpenWeather;
using Newtonsoft.Json.Linq;

int zipCode = 0;
bool isZip = false;

while (!isZip)
{
    Console.Clear();

    try
    {
        Welcome.Message();
        Console.Write("\n\tTo check the current weather forecast,\n\tEnter the zipcode for that area: ");
        isZip = int.TryParse(Console.ReadLine(), out zipCode);

        Weather.GetWeather(zipCode, isZip);

        Console.Write("\n\tPress 'Enter' to search again or 'ESC' to quit: ");
        ConsoleKeyInfo keyStroke = Console.ReadKey(true); // If 'true' is not in the readkey parameter, the ESC key deletes the first char of the next console.writeline
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
