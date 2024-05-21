namespace Zahlenraten
{
    class Program
    {
        static Random myRandom = new Random();
        static int number;
        static int currentNumber;
        static void Main(string[] args)
        {
          
            //1. eine Zufallszahl zwischen 1 und 1000
            number = myRandom.Next(1,1000);
            while(true)
            {
                //2. eine Eingabe vom User
                Console.WriteLine("Gib eine Zahl zwischen 1 und 1000 ein:");
                currentNumber = int.Parse(Console.ReadLine());
                //3. prüfen der Zahl gegen Suchzahl
                if (currentNumber == number)
                {
                    break;
                }
                //4. Ausgabe ob Zahl größer / kleiner
                if (currentNumber > number)
                {
                    Console.WriteLine("Die gesuchte Zahl ist kleiner!");
                }
                if (currentNumber < number)
                {
                    Console.WriteLine("Die gesuchte Zahl ist größer!");
                }

            }
            Console.WriteLine("Zahl gefunden! :) ");



        }

    }

}
