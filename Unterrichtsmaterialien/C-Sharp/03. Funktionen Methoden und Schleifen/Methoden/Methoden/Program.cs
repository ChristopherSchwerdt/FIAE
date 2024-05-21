using System.Net.Http.Headers;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorld();
            WriteToConsole("Nochmal Hallo!");
            LogPersonInfo("Maria", 29);
            LogPersonInfo("Bob", 12);
            LogPersonInfo("Tim", 65);

            int tmp = Sum(1, 1);
            Console.WriteLine(tmp);

            Console.WriteLine(Sum(5, 5));
            Console.WriteLine(Sum(4 , 9));

            Console.WriteLine(GetTen());


        }
        static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }
        static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
        static void LogPersonInfo(string name, int age)
        {
            Console.WriteLine("Your name is " + name + " and your age is: "+ age);
        }
        static int Sum(int x, int y)
        {
            return x + y;
        }
        static int GetTen()
        {
            return 10;
        }
    }
}