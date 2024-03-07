class MainClass
{
    public static void Main(string[] args)
    {
        int number;

        do
        {
            Console.WriteLine("Please enter a number greater than 10: ");
            number = Convert.ToInt32(Console.ReadLine());
        } while (number < 10);

        Console.WriteLine("You entered a number greater than 10: " + number);
    }
}