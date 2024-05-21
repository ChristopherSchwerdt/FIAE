class Program
{
    static void Main()
    {
        int sum = 0;

        for (int i = 1; i <= 100; i++)
        {
            sum += i;
        }

        Console.WriteLine("The sum of the first 100 natural numbers is: " + sum);
    }
}