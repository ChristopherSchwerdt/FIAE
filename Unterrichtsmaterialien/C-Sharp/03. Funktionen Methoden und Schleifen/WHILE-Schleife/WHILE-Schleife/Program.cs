namespace While
{
    class Program
    {

        static void Main(string[] args)
        {
            int i = 1;
            while (i <= 10)
            {
                Console.WriteLine("Die aktuelle Zahl ist "+ i + " bitte drücken Sie Enter");
                Console.ReadLine();
                i++;
         
            }
            
        }
    }
}