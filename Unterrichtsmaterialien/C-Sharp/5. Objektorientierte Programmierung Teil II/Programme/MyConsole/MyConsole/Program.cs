class Program
{
    static void Main(string[] args)
    {   
        //Aufruf der statischen Klasse MyConsole
        MyConsole.WriteLine("Hello World!");

        Console.WriteLine("Hello World!");
    }
}

static class MyConsole
{
    public static void WriteLine(string text)
    {
        Console.WriteLine("my: " +  text);
    }
}