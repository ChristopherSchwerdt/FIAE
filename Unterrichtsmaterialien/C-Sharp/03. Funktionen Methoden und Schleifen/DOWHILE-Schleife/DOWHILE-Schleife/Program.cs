using System.Xml;

namespace DOWHILE
{
    class Program
    {
        static void Main(string[] args)
        {
            int number;
            do
            {
                Console.WriteLine("Bitte geben Sie eine Zahl größer als 10 ein:");
                number = Convert.ToInt32(Console.ReadLine());
            }while(number <= 10);

            Console.WriteLine("Sie haben eine Zahl größer als 10 eingegeben : " + number);
        }
    }
}