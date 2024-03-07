namespace HigherNumber
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //1. Der Benutzer gibt 2 Zahlen ein:
            int zahl1 = 0;
            int zahl2 = 0;

            Console.WriteLine("Hallo! Bitte gib eine Zahl ein:");
            zahl1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Super! Nun noch eine:");
            zahl2 = Convert.ToInt32(Console.ReadLine());

            GetHigherNumber(zahl1, zahl2);

        }
        //2. Eine Funktion, die zwei Zahlen per Parameterliste annimmt und die größere von Beiden ausgibt:
        static void GetHigherNumber(int x, int y) // x = zahl1, y = zahl2
        {
            if (x > y)
            {
                Console.WriteLine("{0} war größer als {1}", x, y);  
            }
            else
            {
                Console.WriteLine(y + " war größer als " + x + "!"+"!");
            }
        }
    }
}