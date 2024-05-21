namespace Hello{

    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Bob", "Charlie", "Maria", "David" };

            foreach(string name in names)
            {
                Console.WriteLine("Hello " + name + "!");
            }
        }
    }
}