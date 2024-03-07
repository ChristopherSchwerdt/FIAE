class MainClass
{
    public static void Main(string[] args)
    {
        List<string> names = new List<string> {"Bob", "Charlie", "David" };

        foreach (string name in names)
        {
            Console.WriteLine("Hello, " + name + "!");
        }
    }
}