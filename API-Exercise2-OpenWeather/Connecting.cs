using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Exercise2_OpenWeather
{
    public class Connecting
    {
        public static void PrintConnection()
        {
            Console.Write("\n\tGetting weather ");
            Thread.Sleep(300);
            for (int i = 0; i < 8; i++)
            {
                Console.Write(". ");
                Thread.Sleep(300);
            }
            Console.Clear();
            Welcome.Message();
        }
    }
}
