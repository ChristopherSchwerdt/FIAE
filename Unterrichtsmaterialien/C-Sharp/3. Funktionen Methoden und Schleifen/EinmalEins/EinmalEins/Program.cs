namespace einmalEins
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welches 1x1 soll ausgegeben werden?");
            int x = Convert.ToInt32(Console.ReadLine());
            int y = 1;
            Console.WriteLine("Das 1x{0} lautet:", x);
            while (y <= 10)
            {
               //Console.WriteLine($"{y} * {x} = {y*x}");
                
                Console.WriteLine(y + "*" + x + "=" + (y * x));
                y++;
            }


        }
    }
}
