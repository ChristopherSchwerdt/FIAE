namespace Average
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bitte gebe eine Zahl ein:");
            float zahl1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Bitte nun die zweite Zahl:");
            float zahl2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Zahl 1: " + zahl1);
            Console.WriteLine("Zahl 2: " + zahl2);

            Console.WriteLine("Der Durchschnitt beträgt "+ GetAverage(zahl1, zahl2));

        }
        static float GetAverage(float x , float y)
        {
            return (x + y) / 2;
        }
    }
}