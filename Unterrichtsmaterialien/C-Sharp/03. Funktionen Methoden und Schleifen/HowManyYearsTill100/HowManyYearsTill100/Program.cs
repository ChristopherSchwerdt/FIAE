namespace HowManyYears
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hey, wie alt bist du ?");
            int age = Convert.ToInt32(Console.ReadLine());

            int dif = getYearsUntil100(age);
            Console.WriteLine($"Du hast noch {dif} Jahre, bis du 100 wirst!");
        }
        static int getYearsUntil100(int age)
        {
            return 100 - age;
            //Console.WriteLine($"Du hast noch {100 - age} Jahre, bis du 100 wirst!");
        }
    }
}