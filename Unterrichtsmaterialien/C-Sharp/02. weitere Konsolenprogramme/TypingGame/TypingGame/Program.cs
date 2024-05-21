using Microsoft.VisualBasic;

namespace Game
{
    class Program
    {
        static List<string> myWords;
        static Random myRandom = new Random();
        static string enteredWord;
        static void Main(string[] args)
        {
            myWords = new List<string>();

            StreamReader myStreamReader = new StreamReader("wordlist.txt");

            //Liste mit Wörtern füllen:
            while(!myStreamReader.EndOfStream)
            {
                string? word = myStreamReader.ReadLine();
                if(!string.IsNullOrWhiteSpace(word))
                {
                    myWords.Add(word);
                }
                
            }
            while(true)
            {
                int index = myRandom.Next(myWords.Count);
                Console.WriteLine("Das zufällig Ausgewähle Wort ist:" + myWords[index]);
                enteredWord = Console.ReadLine();
                if (enteredWord == myWords[index])
                {
                    Console.WriteLine("Super! Das war richtig!");
                }
                else
                {
                    Console.WriteLine("Das war leider falsch!");
                }
                Console.ReadLine();
                Console.Clear();
            }
            




        }
    }
}