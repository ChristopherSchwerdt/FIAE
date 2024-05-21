namespace counter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bis wohin soll ich zählen?");
            int number = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= number; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Ich habe bis {number} gezählt! :) ");

        }
    }
}