namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bitte geben Sie die erste Kante ein:");
            double edge1 =Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Okay, nun die zweite Bitte:");
            double edge2 = Convert.ToDouble(Console.ReadLine());
            //Als Methode:
            RectOrSquare(edge1, edge2);
            //Als Funktion:
            Console.WriteLine("dies ist ein " + RectOrSquareFunc(edge1, edge2));
        }
        //Methode
       static void  RectOrSquare(double x,double y)
        {
            if (x == y)
            {
                Console.WriteLine("Dies ist ein Viereck!");
            }
            else
            {
                Console.WriteLine("Dies ist ein Rechteck!");
            }
        }
        //Funktion
        static string RectOrSquareFunc(double x,double y)
        {
            if (x == y)
            {
                return "viereck";
            }
            else
            {
                return "rechteck";
            }
        }

    }
}