class Program
{




    static Random myRandom = new Random();

    static void Main(string[] args)
    {
              
        for (int i = 0; i < args.Length; i++)
        {
            
            //Console.Write(args[i] + " ");
            foreach(char myChar in args[i])
            {
                Console.ForegroundColor = getRandomColor();
                Console.Write(myChar);

            }
            Console.Write(" ");
        }
        // Zum Schluss wieder alles auf "weiß" setzen.
        Console.ForegroundColor = ConsoleColor.White;
        
       
        
        
    }

  
    private static ConsoleColor getRandomColor()
    {
        return (ConsoleColor)(myRandom.Next(Enum.GetNames(typeof(ConsoleColor)).Length));
    }

}
