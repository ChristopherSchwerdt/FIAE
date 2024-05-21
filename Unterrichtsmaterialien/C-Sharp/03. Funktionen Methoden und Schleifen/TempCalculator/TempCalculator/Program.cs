namespace Temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hast du eine Celsius(1) oder eine Fahrenheit(2) temperatur?");
            int tmp = Convert.ToInt32(Console.ReadLine());
           
            if (tmp == 1)
            {
                Console.WriteLine("Okay, du hast also eine Celsius Temperatur, wie lautet sie?");
                double srcTemp = Convert.ToDouble(Console.ReadLine());
                double temp = ConvertToFahrenheit(srcTemp);
                Console.WriteLine($"Deine Celsius-Temperatur({srcTemp}°C) entspricht {temp} ° Fahrenheit");

            }
            else
            {
                Console.WriteLine("Okay, du hast also eine Fahrenheit Temperatur, wie lautet sie?");
                double srcTemp = Convert.ToDouble(Console.ReadLine());
                double temp = ConvertToCelsius(srcTemp);
                Console.WriteLine($"Deine Fahrenheit-Temperatur({srcTemp}°F) entspricht {temp} ° Celsius");
            }

        }

        private static double ConvertToCelsius(double fahrenheitTemp)
        {
            return (fahrenheitTemp - 32) / 1.8;
        }

        private static double ConvertToFahrenheit(double x)
        {
            return (x * 1.8) + 32;
        }
    }
}