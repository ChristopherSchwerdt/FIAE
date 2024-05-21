
class Program
{
 
    static void Main(string[] args)
    {
        //myEcho.exe Eins zwei drei vier...zehn
        // input = args[0] // eins
        // input = args[1] // zwei
        // args.length = 10


        for(int i = 0; i < args.Length; i++)
        {
            Console.Write(args[i] + " ");
        }    

       

    }
}

